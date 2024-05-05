using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TextToSpeech.Properties;
using TextToSpeech.Services;
using TextToSpeech.Services.ImagePrcessingStages;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Utility;
using TextToSpeech.Views;
using TextToSpeech.Views.UserControlls;
using Windows.Foundation.Collections;
using Windows.Globalization;

namespace TextToSpeech.ViewModels
{
	public class ImageProcessingSettingsViewModel : INotifyPropertyChanged
	{

		#region PRIVATE PROPERTIES
		private ImageProcessingSettingsView _view;
		private Bitmap _imageBitmap;
		private ObservableCollection<Bitmap> _imageBitmapList = new ObservableCollection<Bitmap>();
		private List<IImageProcessingService> _imageProcessingPipeline = new List<IImageProcessingService>();

		private readonly ISnippingScreenshot _snippingScreenshot = new SnippingScreenshot();

		#endregion

		#region PUBLIC PROPERTIES
		public Action CloseAction { get; set; }
		public Bitmap ImageBitmap { get { return _imageBitmap; } private set { _imageBitmap = value; OnPropertyChanged(nameof(ImageBitmap)); } }
		public ObservableCollection<Bitmap> ImageBitmapList { get { return _imageBitmapList; } private set { _imageBitmapList = value; OnPropertyChanged(nameof(ImageBitmapList)); } }
		public ObservableCollection<IImageProcessingService> ImageProcessingPipeline { 
			get
			{	
				var x = new ObservableCollection<IImageProcessingService>(_imageProcessingPipeline);
				return x;
			}
			private set { _imageProcessingPipeline = value.ToList(); OnPropertyChanged(nameof(ImageProcessingPipeline)); }
		}
		public ObservableCollection<EnumImageProcessingStages> ImageProcessingStages { 
			get 
			{
				return new ObservableCollection<EnumImageProcessingStages>(Enum.GetValues(typeof(EnumImageProcessingStages)).Cast<EnumImageProcessingStages>()); 
			} 
		}
		#endregion

		#region COMMANDS
		private RelayCommand<string> _takeScreenshotButtonCommand;
		private RelayCommand<string> _runPipelineButtonCommand;
		private RelayCommand<EnumImageProcessingStages> _addStageToImageProcessingPipeline;
		private RelayCommand<IImageProcessingService> _deleteStageFromImageProcessingPipeline;
		private RelayCommand<IImageProcessingService> _moveUpStageInImageProcessingPipeline;
		private RelayCommand<IImageProcessingService> _moveDownStageInImageProcessingPipeline;
		public RelayCommand<string> TakeScreenshotButtonCommand { get { return _takeScreenshotButtonCommand; } }
		public RelayCommand<string> RunPipelineButtonCommand { get { return _runPipelineButtonCommand; } }
		public RelayCommand<EnumImageProcessingStages> AddStageToImageProcessingPipeline { get { return _addStageToImageProcessingPipeline; } }
		public RelayCommand<IImageProcessingService> DeleteStageFromImageProcessingPipeline { get { return _deleteStageFromImageProcessingPipeline; } }
		public RelayCommand<IImageProcessingService> MoveUpStageInImageProcessingPipeline { get { return _moveUpStageInImageProcessingPipeline; } }
		public RelayCommand<IImageProcessingService> MoveDownStageInImageProcessingPipeline { get { return _moveDownStageInImageProcessingPipeline; } }
		#endregion

		#region CONSTRUTORS
		public ImageProcessingSettingsViewModel(ImageProcessingSettingsView view)
        {
			_view = view;
			_takeScreenshotButtonCommand = new RelayCommand<string>(TakeScreenshotButtonCommandMethod);
			_runPipelineButtonCommand = new RelayCommand<string>(RunPipelineButtonCommandMethod);
			_addStageToImageProcessingPipeline = new RelayCommand<EnumImageProcessingStages>(AddStageToImageProcessingPipelineMethod);
			_deleteStageFromImageProcessingPipeline = new RelayCommand<IImageProcessingService>(DeleteStageFromImageProcessingPipelineDeleteMethod);
			_deleteStageFromImageProcessingPipeline = new RelayCommand<IImageProcessingService>(DeleteStageFromImageProcessingPipelineDeleteMethod);
			_moveUpStageInImageProcessingPipeline = new RelayCommand<IImageProcessingService>(MoveUpStageInImageProcessingPipelineMethod);
			_moveDownStageInImageProcessingPipeline = new RelayCommand<IImageProcessingService>(MoveDownStageInImageProcessingPipelineMethod);
		}
		#endregion

		#region PUBLIC METHODS
		public void TakeScreenshotButtonCommandMethod(string _)
		{
			ImageBitmap = _snippingScreenshot.TakeSnippingScreenshot();
			ImageBitmapList.Clear();
			ImageBitmapList.Add(ImageBitmap);
		}

		public void RunPipelineButtonCommandMethod(string _)
		{
			Bitmap processedImage = _imageBitmap;
			ImageBitmapList.Clear();
			_imageProcessingPipeline.ForEach(step =>
			{
				processedImage = step.ProcessImage(processedImage);
				ImageBitmapList.Add(processedImage);
			});
		}

		public void AddStageToImageProcessingPipelineMethod(EnumImageProcessingStages newStage)
		{
			_imageProcessingPipeline.Add(ImageProcessingStageFactory.CreateService(newStage));
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void DeleteStageFromImageProcessingPipelineDeleteMethod(IImageProcessingService toDelete)
		{
			_imageProcessingPipeline.Remove(toDelete);
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void MoveUpStageInImageProcessingPipelineMethod(IImageProcessingService toMoveUp)
		{
			int oldIndex = _imageProcessingPipeline.IndexOf(toMoveUp);
			_imageProcessingPipeline.MoveItem(toMoveUp, --oldIndex);
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void MoveDownStageInImageProcessingPipelineMethod(IImageProcessingService toMoveDown)
		{
			int oldIndex = _imageProcessingPipeline.IndexOf(toMoveDown);
			_imageProcessingPipeline.MoveItem(toMoveDown, ++oldIndex);
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}

		#endregion

		#region PRIVATE METHODS

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
