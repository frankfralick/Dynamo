using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Dynamo.Models;
using Dynamo.Selection;
using Dynamo.UI;
using Dynamo.Utilities;
using System.Windows.Input;
using Dynamo.Configuration;  
using Dynamo.Graph;
using Dynamo.Graph.Annotations;
using Dynamo.Graph.Connectors;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Notes;
using Dynamo.Graph.Workspaces;
 
using Dynamo.Wpf.ViewModels;

using Function = Dynamo.Graph.Nodes.CustomNodes.Function;

namespace Dynamo.ViewModels
{

    public delegate void NoteEventHandler(object sender, EventArgs e);
    public delegate void ViewEventHandler(object sender, EventArgs e);
    public delegate void SelectionEventHandler(object sender, SelectionBoxUpdateArgs e);
    public delegate void ViewModelAdditionEventHandler(object sender, ViewModelEventArgs e);
    public delegate void WorkspacePropertyEditHandler(WorkspaceModel workspace);

    public enum ShowHideFlags { Hide, Show };

    public partial class WorkspaceViewModel : ViewModelBase
    {
        #region Properties and Fields

        public DynamoViewModel DynamoViewModel { get; private set; }
        public readonly WorkspaceModel Model;

        private bool _canFindNodesFromElements = false;

        public event WorkspaceModel.ZoomEventHandler RequestZoomToViewportCenter;
        public event WorkspaceModel.ZoomEventHandler RequestZoomToViewportPoint;
        public event WorkspaceModel.ZoomEventHandler RequestZoomToFitView;

        public event NodeEventHandler RequestCenterViewOnElement;

        public event ViewEventHandler RequestAddViewToOuterCanvas;
        public event SelectionEventHandler RequestSelectionBoxUpdate;
        public event WorkspacePropertyEditHandler WorkspacePropertyEditRequested;
        public PortViewModel portViewModel { get; set; }
        public bool IsSnapping { get; set; }

        /// <summary>
        /// ViewModel that is used in InCanvasSearch in context menu and called by Shift+DoubleClick.
        /// </summary>
        public SearchViewModel InCanvasSearchViewModel { get; private set; }

        /// <summary>
        /// For requesting registered workspace to zoom in center
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRequestZoomToViewportCenter(object sender, ZoomEventArgs e)
        {
            if (RequestZoomToViewportCenter != null)
            {
                RequestZoomToViewportCenter(this, e);
            }
        }

        /// <summary>
        /// For requesting registered workspace to zoom in out from a point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnRequestZoomToViewportPoint(object sender, ZoomEventArgs e)
        {
            if (RequestZoomToViewportPoint != null)
            {
                RequestZoomToViewportPoint(this, e);
            }
        }

        /// <summary>
        /// For requesting registered workspace to zoom in or out to fitview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRequestZoomToFitView(object sender, ZoomEventArgs e)
        {
            if (RequestZoomToFitView != null)
            {
                RequestZoomToFitView(this, e);
            }
        }

        internal virtual void OnRequestCenterViewOnElement(object sender, ModelEventArgs e)
        {
            if (RequestCenterViewOnElement != null)
                RequestCenterViewOnElement(this, e);
        }


        public virtual void OnRequestAddViewToOuterCanvas(object sender, ViewEventArgs e)
        {
            if (RequestAddViewToOuterCanvas != null)
                RequestAddViewToOuterCanvas(this, e);
        }

        public virtual void OnRequestSelectionBoxUpdate(object sender, SelectionBoxUpdateArgs e)
        {
            if (RequestSelectionBoxUpdate != null)
                RequestSelectionBoxUpdate(this, e);
        }

        public virtual void OnWorkspacePropertyEditRequested()
        {
            // extend this for all workspaces
            if (WorkspacePropertyEditRequested != null)
                WorkspacePropertyEditRequested(Model);
        }

        internal event Action<ShowHideFlags> RequestShowInCanvasSearch;

        private void OnRequestShowInCanvasSearch(object param)
        {
            var flag = (ShowHideFlags)param;

            if (RequestShowInCanvasSearch != null)
                RequestShowInCanvasSearch(flag);
        }

        /// <summary>
        /// Cursor Property Binding for WorkspaceView
        /// </summary>
        private Cursor currentCursor = null;
        public Cursor CurrentCursor
        {
            get { return currentCursor; }
            set { currentCursor = value; RaisePropertyChanged("CurrentCursor"); }
        }

        /// <summary>
        /// Force Cursor Property Binding for WorkspaceView
        /// </summary>
        private bool isCursorForced = false;
        public bool IsCursorForced
        {
            get { return isCursorForced; }
            set { isCursorForced = value; RaisePropertyChanged("IsCursorForced"); }
        }

        private CompositeCollection _workspaceElements = new CompositeCollection();
        public CompositeCollection WorkspaceElements { get { return _workspaceElements; } }

        ObservableCollection<ConnectorViewModel> _connectors = new ObservableCollection<ConnectorViewModel>();
        private ObservableCollection<Watch3DFullscreenViewModel> _watches = new ObservableCollection<Watch3DFullscreenViewModel>();
        ObservableCollection<NodeViewModel> _nodes = new ObservableCollection<NodeViewModel>();
        ObservableCollection<NoteViewModel> _notes = new ObservableCollection<NoteViewModel>();
        ObservableCollection<InfoBubbleViewModel> _errors = new ObservableCollection<InfoBubbleViewModel>();
        ObservableCollection<AnnotationViewModel> _annotations = new ObservableCollection<AnnotationViewModel>();

        public ObservableCollection<ConnectorViewModel> Connectors { get { return _connectors; } }
        public ObservableCollection<NodeViewModel> Nodes { get { return _nodes; } }
        public ObservableCollection<NoteViewModel> Notes { get { return _notes; } }
        public ObservableCollection<InfoBubbleViewModel> Errors { get { return _errors; } }
        public ObservableCollection<AnnotationViewModel> Annotations { get { return _annotations; } }

        public string Name
        {
            get
            {
                if (Model == DynamoViewModel.HomeSpace)
                    return "Home";
                return Model.Name;
            }
        }

        public string FileName
        {
            get { return Model.FileName; }
        }

        public bool CanEditName
        {
            get { return Model != DynamoViewModel.HomeSpace; }
        }

        public bool IsCurrentSpace
        {
            get { return Model == DynamoViewModel.CurrentSpace; }
        }

        public bool IsHomeSpace
        {
            get { return Model == DynamoViewModel.HomeSpace; }
        }

        public bool HasUnsavedChanges
        {
            get { return Model.HasUnsavedChanges; }
            set { Model.HasUnsavedChanges = value; }
        }

        public ObservableCollection<Watch3DFullscreenViewModel> Watch3DViewModels
        {
            get { return _watches; }
            set
            {
                _watches = value;
                RaisePropertyChanged("Watch3DViewModels");
            }
        }

        public double Zoom
        {
            get { return Model.Zoom; }
        }

        public bool CanZoomIn
        {
            get { return CanZoom(Configurations.ZoomIncrement); }
        }

        public bool CanZoomOut
        {
            get { return CanZoom(-Configurations.ZoomIncrement); }
        }

        internal void ZoomInInternal()
        {
            var args = new ZoomEventArgs(Configurations.ZoomIncrement);
            OnRequestZoomToViewportCenter(this, args);
            ResetFitViewToggle(null);
        }

        internal void ZoomOutInternal()
        {
            var args = new ZoomEventArgs(-Configurations.ZoomIncrement);
            OnRequestZoomToViewportCenter(this, args);
            ResetFitViewToggle(null);
        }

        public bool CanFindNodesFromElements
        {
            get { return _canFindNodesFromElements; }
            set
            {
                _canFindNodesFromElements = value;
                RaisePropertyChanged("CanFindNodesFromElements");
            }
        }

        public bool CanShowInfoBubble
        {
            get { return stateMachine.IsInIdleState; }
        }

        public bool CanRunNodeToCode
        {
            get
            {
                return true;
            }
        }

        public Action FindNodesFromElements { get; set; }

        public RunSettingsViewModel RunSettingsViewModel { get; protected set; }
         
        #endregion

        public WorkspaceViewModel(WorkspaceModel model, DynamoViewModel dynamoViewModel)
        {
            this.DynamoViewModel = dynamoViewModel;
            this.DynamoViewModel.PropertyChanged += DynamoViewModel_PropertyChanged;

            Model = model;
            stateMachine = new StateMachine(this);

            var nodesColl = new CollectionContainer { Collection = Nodes };
            _workspaceElements.Add(nodesColl);

            var connColl = new CollectionContainer { Collection = Connectors };
            _workspaceElements.Add(connColl);

            var notesColl = new CollectionContainer { Collection = Notes };
            _workspaceElements.Add(notesColl);

            var errorsColl = new CollectionContainer { Collection = Errors };
            _workspaceElements.Add(errorsColl);

            var annotationsColl = new CollectionContainer {Collection = Annotations};
            _workspaceElements.Add(annotationsColl);

            //respond to collection changes on the model by creating new view models
            //currently, view models are added for notes and nodes
            //connector view models are added during connection

            Model.NodeAdded += Model_NodeAdded;
            Model.NodeRemoved += Model_NodeRemoved;
            Model.NodesCleared += Model_NodesCleared;

            Model.NoteAdded += Model_NoteAdded;
            Model.NoteRemoved += Model_NoteRemoved;
            Model.NotesCleared += Model_NotesCleared;

            Model.AnnotationAdded += Model_AnnotationAdded;
            Model.AnnotationRemoved += Model_AnnotationRemoved;
            Model.AnnotationsCleared += Model_AnnotationsCleared;

            Model.ConnectorAdded += Connectors_ConnectorAdded;
            Model.ConnectorDeleted += Connectors_ConnectorDeleted;
            Model.PropertyChanged += ModelPropertyChanged;

            DynamoSelection.Instance.Selection.CollectionChanged +=
                (sender, e) => RefreshViewOnSelectionChange();

            DynamoViewModel.CopyCommand.CanExecuteChanged += CopyPasteChanged;
            DynamoViewModel.PasteCommand.CanExecuteChanged += CopyPasteChanged;

            // sync collections

            foreach (NodeModel node in Model.Nodes) Model_NodeAdded(node);
            foreach (NoteModel note in Model.Notes) Model_NoteAdded(note);
            foreach (AnnotationModel annotation in Model.Annotations) Model_AnnotationAdded(annotation);
            foreach (ConnectorModel connector in Model.Connectors) Connectors_ConnectorAdded(connector);

            InCanvasSearchViewModel = new SearchViewModel(DynamoViewModel);
            InCanvasSearchViewModel.Visible = true;
        }

        void CopyPasteChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CanPaste", "CanCopy", "CanCopyOrPaste");
            PasteCommand.RaiseCanExecuteChanged();
        }

        void RunSettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // If any property changes on the run settings object
            // Raise a property change notification for the RunSettingsViewModel
            // property
            RaisePropertyChanged("RunSettingsViewModel");
        }

        void DynamoViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentSpace":
                    // When workspace is changed(e.g. from home to custom), close InCanvasSearch.
                    OnRequestShowInCanvasSearch(ShowHideFlags.Hide);
                    break;                
            }
        }

        void Connectors_ConnectorAdded(ConnectorModel c)
        {
            var viewModel = new ConnectorViewModel(this, c);
            if (_connectors.All(x => x.ConnectorModel != c))
                _connectors.Add(viewModel);
        }

        void Connectors_ConnectorDeleted(ConnectorModel c)
        {
            var connector = _connectors.FirstOrDefault(x => x.ConnectorModel == c);
            if (connector != null)
                _connectors.Remove(connector);
        }

        private void Model_NoteAdded(NoteModel note)
        {
            var viewModel = new NoteViewModel(this, note);
            _notes.Add(viewModel);
        }

        private void Model_NoteRemoved(NoteModel note)
        {
            _notes.Remove(_notes.First(x => x.Model == note));
        }

        private void Model_NotesCleared()
        {
            _notes.Clear();
        }

        private void Model_AnnotationAdded(AnnotationModel annotation)
        {
            var viewModel = new AnnotationViewModel(this, annotation);
            _annotations.Add(viewModel);
        }

        private void Model_AnnotationRemoved(AnnotationModel annotation)
        {
            _annotations.Remove(_annotations.First(x => x.AnnotationModel == annotation));
        }

        private void Model_AnnotationsCleared()
        {
            _annotations.Clear();
        }


        void Model_NodesCleared()
        {
            _nodes.Clear();
            Errors.Clear();
            PostNodeChangeActions();
        }

        void Model_NodeRemoved(NodeModel node)
        {
            NodeViewModel nodeViewModel = _nodes.First(x => x.NodeLogic == node);
            Errors.Remove(nodeViewModel.ErrorBubble);
            _nodes.Remove(nodeViewModel);

            PostNodeChangeActions();
        }

        void Model_NodeAdded(NodeModel node)
        {
            var nodeViewModel = new NodeViewModel(this, node);
            nodeViewModel.SnapInputEvent += nodeViewModel_SnapInputEvent;
            nodeViewModel.NodeLogic.Modified += OnNodeModified;
            _nodes.Add(nodeViewModel);
            Errors.Add(nodeViewModel.ErrorBubble);
            nodeViewModel.UpdateBubbleContent();

            PostNodeChangeActions();
        }

        void PostNodeChangeActions()
        {
            if (RunSettingsViewModel == null) return;
            CheckAndSetPeriodicRunCapability();
        }

        /// <summary>
        /// This is required here to compute the nodes delta state.
        /// This is overriden in HomeWorkspaceViewModel
        /// </summary>
        /// <param name="obj">The object.</param>
        public virtual void OnNodeModified(NodeModel obj)
        {
            
        }

        internal void CheckAndSetPeriodicRunCapability()
        {
            var periodUpdateAvailable = Model.Nodes.Any(n => n.CanUpdatePeriodically);
            RunSettingsViewModel.ToggleRunTypeEnabled(RunType.Periodic, periodUpdateAvailable);
        }

        /// <summary>
        /// Handles the port snapping on Mouse Enter.
        /// </summary>
        /// <param name="portViewModel">The port view model.</param>
        /// <param name="eventType">Type of the event.</param>
        private void nodeViewModel_SnapInputEvent(PortViewModel portViewModel)
        {
            switch (portViewModel.EventType)
            {
                case PortEventType.MouseEnter:                    
                    IsSnapping = this.CheckActiveConnectorCompatibility(portViewModel);
                    this.portViewModel = portViewModel;
                    break;
                case PortEventType.MouseLeave:
                    IsSnapping = this.CheckActiveConnectorCompatibility(portViewModel, false);
                    this.portViewModel = portViewModel;
                    break;
                case PortEventType.MouseLeftButtonDown:
                    //If the connector is not active, then the state is changed to None. otherwise, the connector state is connection and 
                    //is not deleted from the view.
                    this.portViewModel = portViewModel;
                    if (this.CheckActiveConnectorCompatibility(portViewModel))
                    {
                        this.HandlePortClicked(portViewModel);
                    }
                    else
                    {
                        this.CancelActiveState();

                    }
                    break;
                default:
                    IsSnapping = this.CheckActiveConnectorCompatibility(portViewModel);
                    this.portViewModel = portViewModel;
                    break;

            }
            
        }

        void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    RaisePropertyChanged("Name");
                    break;
                case "X":
                    break;
                case "Y":
                    break;
                case "Zoom":
                    this.Model.OnZoomChanged(this, new ZoomEventArgs(Model.Zoom));
                    RaisePropertyChanged("Zoom");
                    break;
                case "IsCurrentSpace":
                    RaisePropertyChanged("IsCurrentSpace");
                    RaisePropertyChanged("IsHomeSpace");
                    break;
                case "HasUnsavedChanges":
                    RaisePropertyChanged("HasUnsavedChanges");
                    break;
                case "FileName":
                    RaisePropertyChanged("FileName");
                    break;
            }
        }

        internal void SelectAll(object parameter)
        {
            DynamoSelection.Instance.ClearSelection();
            Nodes.ToList().ForEach((ele) => DynamoSelection.Instance.Selection.Add(ele.NodeModel));
        }

        internal bool CanSelectAll(object parameter)
        {
            return true;
        }

        /// <summary>
        /// After command framework is implemented, this method should now be only 
        /// called from a menu item (i.e. Ctrl + W). It should not be used as a 
        /// way for any other code paths to convert nodes to code programmatically. 
        /// For that we now have ConvertNodesToCodeInternal which takes in more 
        /// configurable arguments.
        /// </summary>
        /// <param name="parameters">This is not used and should always be null,
        /// otherwise an ArgumentException will be thrown.</param>
        /// 
        internal void NodeToCode(object parameters)
        {
            if (null != parameters) // See above for details of this exception.
            {
                const string message = "Internal error, argument must be null";
                throw new ArgumentException(message, "parameters");
            }

            var command = new DynamoModel.ConvertNodesToCodeCommand();
            this.DynamoViewModel.ExecuteCommand(command);
        }

        internal bool CanNodeToCode(object parameters)
        {
            return DynamoSelection.Instance.Selection.OfType<NodeModel>().Any();
        }

        internal void SelectInRegion(Rect2D region, bool isCrossSelect)
        {
            var fullyEnclosed = !isCrossSelect;
            var selection = DynamoSelection.Instance.Selection;
            var childlessModels = Model.Nodes.Concat<ModelBase>(Model.Notes);

            foreach (var n in childlessModels)
            {
                if (IsInRegion(region, n, fullyEnclosed))
                {
                    selection.AddUnique(n);
                }
                else if (n.IsSelected)
                {
                    selection.Remove(n);
                }
            }

            foreach (var n in Model.Annotations)
            {
                if (IsInRegion(region, n, fullyEnclosed))
                {
                    selection.AddUnique(n);
                    // if annotation is selected its children should be added to selection too
                    foreach (var m in n.SelectedModels)
                    {
                        selection.AddUnique(m);
                    }
                }
                else if (n.IsSelected)
                {
                    selection.Remove(n);
                }
            }
        }

        private static bool IsInRegion(Rect2D region, ILocatable locatable, bool fullyEnclosed)
        {
            double x0 = locatable.X;
            double y0 = locatable.Y;

            if (false == fullyEnclosed) // Cross selection.
            {
                var test = new Rect2D(x0, y0, locatable.Width, locatable.Height);
                return region.IntersectsWith(test);
            }

            double x1 = x0 + locatable.Width;
            double y1 = y0 + locatable.Height;
            return (region.Contains(x0, y0) && region.Contains(x1, y1));
        }

        public double GetSelectionAverageX()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.CenterX)
                           .Average();
        }

        public double GetSelectionAverageY()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.CenterY)
                           .Average();
        }

        public double GetSelectionMinX()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.X)
                           .Min();
        }

        public double GetSelectionMinY()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.Y)
                           .Min();
        }

        public double GetSelectionMaxX()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.X + x.Width)
                           .Max();
        }

        public double GetSelectionMaxLeftX()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.X)
                           .Max();
        }

        public double GetSelectionMaxY()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.Y + x.Height)
                           .Max();
        }

        public double GetSelectionMaxTopY()
        {
            return DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                           .Cast<ILocatable>()
                           .Select((x) => x.Y)
                           .Max();
        }

        public void AlignSelected(object parameter)
        {
            string alignType = parameter.ToString();

            if (DynamoSelection.Instance.Selection.Count <= 1) return;

            // All the models in the selection will be modified, 
            // record their current states before anything gets changed.
            SmartCollection<ISelectable> selection = DynamoSelection.Instance.Selection;
            IEnumerable<ModelBase> models = selection.OfType<ModelBase>();
            WorkspaceModel.RecordModelsForModification(models.ToList(), Model.UndoRecorder);

            var toAlign = DynamoSelection.Instance.Selection.OfType<ILocatable>().ToList();

            switch (alignType)
            {
                case "HorizontalCenter":
                {
                    var xAll = GetSelectionAverageX();
                    toAlign.ForEach((x) => { x.CenterX = xAll; });
                }
                    break;
                case "HorizontalLeft":
                {
                    var xAll = GetSelectionMinX();
                    toAlign.ForEach((x) => { x.X = xAll; });
                }
                    break;
                case "HorizontalRight":
                {
                    var xAll = GetSelectionMaxX();
                    toAlign.ForEach((x) => { x.X = xAll - x.Width; });
                }
                    break;
                case "VerticalCenter":
                {
                    var yAll = GetSelectionAverageY();
                    toAlign.ForEach((x) => { x.CenterY = yAll; });
                }
                    break;
                case "VerticalTop":
                {
                    var yAll = GetSelectionMinY();
                    toAlign.ForEach((x) => { x.Y = yAll; });
                }
                    break;
                case "VerticalBottom":
                {
                    var yAll = GetSelectionMaxY();
                    toAlign.ForEach((x) => { x.Y = yAll - x.Height; });
                }
                    break;
                case "VerticalDistribute":
                {
                    if (DynamoSelection.Instance.Selection.Count <= 2) return;

                    var yMin = GetSelectionMinY();
                    var yMax = GetSelectionMaxY();

                    var spacing = 0.0;
                    var span = yMax - yMin;

                    var nodeHeightSum =
                        DynamoSelection.Instance.Selection.Where(y => y is ILocatable)
                            .Cast<ILocatable>()
                            .Sum((y) => y.Height);

                    if (span > nodeHeightSum)
                    {
                        spacing = (span - nodeHeightSum)
                            /(DynamoSelection.Instance.Selection.Count - 1);
                    }

                    var cursor = yMin;
                    foreach (var node in toAlign.OrderBy(y => y.Y))
                    {
                        node.Y = cursor;
                        cursor += node.Height + spacing;
                    }
                }
                    break;
                case "HorizontalDistribute":
                {
                    if (DynamoSelection.Instance.Selection.Count <= 2) return;

                    var xMin = GetSelectionMinX();
                    var xMax = GetSelectionMaxX();

                    var spacing = 0.0;
                    var span = xMax - xMin;
                    var nodeWidthSum =
                        DynamoSelection.Instance.Selection.Where((x) => x is ILocatable)
                            .Cast<ILocatable>()
                            .Sum((x) => x.Width);

                    // If there is more span than total node width,
                    // distribute the nodes with a gap. If not, leave
                    // the spacing at 0 and the nodes will distribute
                    // up against each other.
                    if (span > nodeWidthSum)
                    {
                        spacing = (span - nodeWidthSum)
                            /(DynamoSelection.Instance.Selection.Count - 1);
                    }

                    var cursor = xMin;
                    foreach (var node in toAlign.OrderBy(x => x.X))
                    {
                        node.X = cursor;
                        cursor += node.Width + spacing;
                    }
                }
                    break;
            }

            toAlign.ForEach(x => x.ReportPosition());
        }

        private static bool CanAlignSelected(object parameter)
        {
            return DynamoSelection.Instance.Selection.Count > 1;
        }
        
        private void Paste(object param)
        {
            var point = InCanvasSearchViewModel.InCanvasSearchPosition;
            DynamoViewModel.Model.Paste(new Point2D(point.X, point.Y), false);
            DynamoViewModel.RaiseCanExecuteUndoRedo();
        }

        private void ShowHideAllGeometryPreview(object parameter)
        {
            var modelGuids = DynamoSelection.Instance.Selection.
                OfType<NodeModel>().Select(n => n.GUID);

            if (!modelGuids.Any())
                return;

            var command = new DynamoModel.UpdateModelValueCommand(Guid.Empty,
                modelGuids, "IsVisible", (string) parameter);

            DynamoViewModel.Model.ExecuteCommand(command);
            RefreshViewOnSelectionChange();
        }

        private void ShowHideAllUpstreamPreview(object parameter)
        {
            var modelGuids = DynamoSelection.Instance.Selection.
                OfType<NodeModel>().Select(n => n.GUID);

            if (!modelGuids.Any())
                return;

            var command = new DynamoModel.UpdateModelValueCommand(Guid.Empty,
                modelGuids, "IsUpstreamVisible", (string) parameter);

            DynamoViewModel.Model.ExecuteCommand(command);
            RefreshViewOnSelectionChange();
        }

        private void SetArgumentLacing(object parameter)
        {
            var modelGuids = DynamoSelection.Instance.Selection.
                OfType<NodeModel>().Select(n => n.GUID);

            if (!modelGuids.Any())
                return;

            var command = new DynamoModel.UpdateModelValueCommand(Guid.Empty,
                modelGuids, "ArgumentLacing", (string) parameter);

            DynamoViewModel.Model.ExecuteCommand(command);
            RaisePropertyChanged("SelectionArgumentLacing");
        }

        private void Hide(object parameters)
        {
            // Closing of custom workspaces will simply close those workspaces,
            // but closing Home workspace has a different meaning. First off, 
            // Home workspace cannot be closed or hidden, it can only be cleared.
            // As of this revision, pressing the "X" button on Home workspace 
            // tab simply clears the Home workspace, and bring up the Start Page
            // if there are no other custom workspace that is opened.
            // 

            if (this.IsHomeSpace)
            {
                if (DynamoViewModel.CloseHomeWorkspaceCommand.CanExecute(null))
                    DynamoViewModel.CloseHomeWorkspaceCommand.Execute(null);
            }
            else
            {
                if (!Model.HasUnsavedChanges || DynamoViewModel.AskUserToSaveWorkspaceOrCancel(Model))
                    DynamoViewModel.Model.RemoveWorkspace(Model);
            }
        }

        private static bool CanHide(object parameters)
        {
            // Workspaces other than HOME can be hidden (i.e. closed), but we 
            // are enabling it also for the HOME workspace. When clicked, the 
            // HOME workspace is cleared (i.e. equivalent of pressing the New 
            // button), and if there is no other workspaces opened, then the 
            // Start Page is displayed.
            // 
            return true;
        }

        private void SetCurrentOffset(object parameter)
        {
            var p = (Point)parameter;

            //set the current offset without triggering
            //any property change notices.
            if (Model.X != p.X && Model.Y != p.Y)
            {
                Model.X = p.X;
                Model.Y = p.Y;
            }
        }

        private static bool CanSetCurrentOffset(object parameter)
        {
            return true;
        }

        private void CreateNodeFromSelection(object parameter)
        {
            CollapseNodes(
                DynamoSelection.Instance.Selection.Where(x => x is NodeModel)
                    .Select(x => (x as NodeModel)));
        }

        //private void NodeFromSelectionCanExecuteChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    NodeFromSelectionCommand.RaiseCanExecuteChanged();
        //}

        private void AlignSelectionCanExecuteChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AlignSelectedCommand.RaiseCanExecuteChanged();

        }

        private static bool CanCreateNodeFromSelection(object parameter)
        {
            return DynamoSelection.Instance.Selection.OfType<NodeModel>().Any();
        }

        private bool CanZoom(double zoom)
        {
            return (!(zoom < 0) || !(Model.Zoom <= WorkspaceModel.ZOOM_MINIMUM)) && (!(zoom > 0) 
                || !(Model.Zoom >= WorkspaceModel.ZOOM_MAXIMUM));
        }

        private void SetZoom(object zoom)
        {
            Model.Zoom = Convert.ToDouble(zoom);
        }

        private static bool CanSetZoom(object zoom)
        {
            double setZoom = Convert.ToDouble(zoom);
            return setZoom >= WorkspaceModel.ZOOM_MINIMUM && setZoom <= WorkspaceModel.ZOOM_MAXIMUM;
        }

        private bool _fitViewActualZoomToggle = false;

        internal void FitViewInternal()
        {
            // Get the offset and focus width & height (zoom if 100%)
            double minX, maxX, minY, maxY;

            // Get the width and height of area to fit
            if (DynamoSelection.Instance.Selection.Count > 0)
            {   // has selection
                minX = GetSelectionMinX();
                maxX = GetSelectionMaxX();
                minY = GetSelectionMinY();
                maxY = GetSelectionMaxY();
            }
            else
            {   // no selection, fitview all nodes
                if (!_nodes.Any()) return;

                List<NodeModel> nodes = _nodes.Select(x => x.NodeModel).Where(x => x != null).ToList();
                minX = nodes.Select(x => x.X).Min();
                maxX = nodes.Select(x => x.X + x.Width).Max();
                minY = nodes.Select(y => y.Y).Min();
                maxY = nodes.Select(y => y.Y + y.Height).Max();
            }

            var offset = new Point2D(minX, minY);
            double focusWidth = maxX - minX;
            double focusHeight = maxY - minY;

            _fitViewActualZoomToggle = !_fitViewActualZoomToggle;
            ZoomEventArgs zoomArgs = _fitViewActualZoomToggle
                ? new ZoomEventArgs(offset, focusWidth, focusHeight)
                : new ZoomEventArgs(offset, focusWidth, focusHeight, 1.0);

            OnRequestZoomToFitView(this, zoomArgs);
        }

        private void ResetFitViewToggle(object o)
        {
            _fitViewActualZoomToggle = false;
        }

        private static bool CanResetFitViewToggle(object o)
        {
            return true;
        }

        private void FindById(object id)
        {
            try
            {
                var node = DynamoViewModel.Model.CurrentWorkspace.Nodes.First(x => x.GUID.ToString() == id.ToString());

                if (node != null)
                {
                    //select the element
                    DynamoSelection.Instance.ClearSelection();
                    DynamoSelection.Instance.Selection.Add(node);

                    //focus on the element
                    DynamoViewModel.ShowElement(node);

                    return;
                }
            }
            catch
            {
                DynamoViewModel.Model.Logger.Log(Wpf.Properties.Resources.MessageFailedToFindNodeById);
            }

            try
            {
                var function =
                    (Function)DynamoViewModel.Model.CurrentWorkspace.Nodes.First(x => x is Function && ((Function)x).Definition.FunctionId.ToString() == id.ToString());

                if (function == null) return;

                //select the element
                DynamoSelection.Instance.ClearSelection();
                DynamoSelection.Instance.Selection.Add(function);

                //focus on the element
                DynamoViewModel.ShowElement(function);
            }
            catch
            {
                DynamoViewModel.Model.Logger.Log(Wpf.Properties.Resources.MessageFailedToFindNodeById);
            }
        }

        private static bool CanFindById(object id)
        {
            return !string.IsNullOrEmpty(id.ToString());
        }

        private void FindNodesFromSelection(object parameter)
        {
            FindNodesFromElements();
        }

        private bool CanFindNodesFromSelection(object parameter)
        {
            return FindNodesFromElements != null;
        }

        private void DoGraphAutoLayout(object o)
        {
            Model.DoGraphAutoLayout();
            DynamoViewModel.RaiseCanExecuteUndoRedo();
        }

        private static bool CanDoGraphAutoLayout(object o)
        {
            return true;
        }

        /// <summary>
        ///     Collapse a set of nodes in this workspace
        /// </summary>
        /// <param name="selectedNodes"> The function definition for the user-defined node </param>
        internal void CollapseNodes(IEnumerable<NodeModel> selectedNodes)
        {
            var args = new FunctionNamePromptEventArgs();
            DynamoViewModel.Model.OnRequestsFunctionNamePrompt(null, args);

            if (!args.Success)
                return;

            DynamoViewModel.Model.AddCustomNodeWorkspace(
                DynamoViewModel.Model.CustomNodeManager.Collapse(
                    selectedNodes, Model, DynamoModel.IsTestMode, args));
        }

        internal void Loaded()
        {
            RaisePropertyChanged("IsHomeSpace");

            // New workspace or swapped workspace to follow it offset and zoom
            this.Model.OnCurrentOffsetChanged(this, new PointEventArgs(new Point2D(Model.X, Model.Y)));
            this.Model.OnZoomChanged(this, new ZoomEventArgs(Model.Zoom));
        }

        private void RefreshViewOnSelectionChange()
        {
            AlignSelectedCommand.RaiseCanExecuteChanged();
            ShowHideAllUpstreamPreviewCommand.RaiseCanExecuteChanged();
            ShowHideAllGeometryPreviewCommand.RaiseCanExecuteChanged();
            SetArgumentLacingCommand.RaiseCanExecuteChanged();           
            RaisePropertyChanged("HasSelection");
            RaisePropertyChanged("IsGeometryOperationEnabled");
            RaisePropertyChanged("AnyNodeVisible");
            RaisePropertyChanged("AnyNodeUpstreamVisible");
            RaisePropertyChanged("SelectionArgumentLacing");            
        }
    }

    public class ViewModelEventArgs : EventArgs
    {
        public NodeViewModel ViewModel { get; set; }
        public ViewModelEventArgs(NodeViewModel vm)
        {
            ViewModel = vm;
        }
    }
}
