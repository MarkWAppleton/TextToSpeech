using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services;
using TextToSpeech.Services.ImagePrcessingServices;
using TextToSpeech.Services.ImagePrcessingStages;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Utility;
using TextToSpeech.Views;

namespace TextToSpeech.ViewModels
{
	public class ImageProcessingSettingsViewModel : INotifyPropertyChanged
	{

		#region PRIVATE PROPERTIES
		private ImageProcessingSettingsView _view;
		private Bitmap _imageBitmap;
		private ObservableCollection<Bitmap> _imageBitmapList = new ObservableCollection<Bitmap>();
		private List<ImageProcessingStage> _imageProcessingPipeline = new List<ImageProcessingStage>();
		private readonly ISnippingScreenshot _snippingScreenshot = new SnippingScreenshot();

		#endregion

		#region PUBLIC PROPERTIES
		public Action CloseAction { get; set; }
		public Bitmap ImageBitmap { get { return _imageBitmap; } private set { _imageBitmap = value; OnPropertyChanged(nameof(ImageBitmap)); } }
		public ObservableCollection<Bitmap> ImageBitmapList { get { return _imageBitmapList; } private set { _imageBitmapList = value; OnPropertyChanged(nameof(ImageBitmapList)); } }
		public ObservableCollection<ImageProcessingStage> ImageProcessingPipeline { 
			get
			{	
				var x = new ObservableCollection<ImageProcessingStage>(_imageProcessingPipeline);
				return x;
			}
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
		private RelayCommand<ImageProcessingStage> _deleteStageFromImageProcessingPipeline;
		private RelayCommand<ImageProcessingStage> _moveUpStageInImageProcessingPipeline;
		private RelayCommand<ImageProcessingStage> _moveDownStageInImageProcessingPipeline;
		public RelayCommand<string> TakeScreenshotButtonCommand { get { return _takeScreenshotButtonCommand; } }
		public RelayCommand<string> RunPipelineButtonCommand { get { return _runPipelineButtonCommand; } }
		public RelayCommand<EnumImageProcessingStages> AddStageToImageProcessingPipeline { get { return _addStageToImageProcessingPipeline; } }
		public RelayCommand<ImageProcessingStage> DeleteStageFromImageProcessingPipeline { get { return _deleteStageFromImageProcessingPipeline; } }
		public RelayCommand<ImageProcessingStage> MoveUpStageInImageProcessingPipeline { get { return _moveUpStageInImageProcessingPipeline; } }
		public RelayCommand<ImageProcessingStage> MoveDownStageInImageProcessingPipeline { get { return _moveDownStageInImageProcessingPipeline; } }
		#endregion

		#region CONSTRUTORS
		public ImageProcessingSettingsViewModel(ImageProcessingSettingsView view)
        {
			_view = view;
			_takeScreenshotButtonCommand = new RelayCommand<string>(TakeScreenshotButtonCommandMethod);
			_runPipelineButtonCommand = new RelayCommand<string>(RunPipelineButtonCommandMethod);
			_addStageToImageProcessingPipeline = new RelayCommand<EnumImageProcessingStages>(AddStageToImageProcessingPipelineMethod);
			_deleteStageFromImageProcessingPipeline = new RelayCommand<ImageProcessingStage>(DeleteStageFromImageProcessingPipelineDeleteMethod);
			_deleteStageFromImageProcessingPipeline = new RelayCommand<ImageProcessingStage>(DeleteStageFromImageProcessingPipelineDeleteMethod);
			_moveUpStageInImageProcessingPipeline = new RelayCommand<ImageProcessingStage>(MoveUpStageInImageProcessingPipelineMethod);
			_moveDownStageInImageProcessingPipeline = new RelayCommand<ImageProcessingStage>(MoveDownStageInImageProcessingPipelineMethod);
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
			ImageBitmapList.Add(_imageBitmap);
			_imageProcessingPipeline.ForEach(step =>
			{
				processedImage = step.ImageProcessingService.ProcessImage(processedImage, step.ImageProcessingConfig);
				ImageBitmapList.Add(processedImage);
			});
		}

		public void AddStageToImageProcessingPipelineMethod(EnumImageProcessingStages newStage)
		{
			_imageProcessingPipeline.Add(new ImageProcessingStage()
			{
				ImageProcessingService = ImageProcessingServiceFactory.CreateService(newStage),
				ImageProcessingConfig = ImageProcessingConfigFactory.CreateConfig(newStage),
			});
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void DeleteStageFromImageProcessingPipelineDeleteMethod(ImageProcessingStage toDelete)
		{
			_imageProcessingPipeline.Remove(toDelete);
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void MoveUpStageInImageProcessingPipelineMethod(ImageProcessingStage toMoveUp)
		{
			int oldIndex = _imageProcessingPipeline.IndexOf(toMoveUp);
			_imageProcessingPipeline.MoveItem(toMoveUp, --oldIndex);
			OnPropertyChanged(nameof(ImageProcessingPipeline));
		}
		public void MoveDownStageInImageProcessingPipelineMethod(ImageProcessingStage toMoveDown)
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
