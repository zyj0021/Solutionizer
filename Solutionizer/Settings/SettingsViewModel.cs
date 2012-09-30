﻿using System;
using System.Linq.Expressions;
using Caliburn.Micro;
using Solutionizer.Services;

namespace Solutionizer.Settings {
    public sealed class SettingsViewModel : Screen {
        private readonly ISettings _settings;
        private bool _scanOnStartup;
        private bool _simplifyProjectTree;
        private bool _includeReferencedProjects;
        private bool _isFlatMode;
        private string _referenceFolderName;
        private int _referenceTreeDepth;
        private VisualStudioVersion _visualStudioVersion;

        public SettingsViewModel(ISettings settings) {
            _settings = settings;
            DisplayName = "Settings";
        }

        protected override void OnActivate() {
            base.OnActivate();

            ScanOnStartup = _settings.ScanOnStartup;
            SimplifyProjectTree = _settings.SimplifyProjectTree;
            IncludeReferencedProjects = _settings.IncludeReferencedProjects;
            ReferenceFolderName = _settings.ReferenceFolderName;
            ReferenceTreeDepth = _settings.ReferenceTreeDepth;
            IsFlatMode = _settings.IsFlatMode;
            VisualStudioVersion = _settings.VisualStudioVersion;
        }

        public bool ScanOnStartup {
            get { return _scanOnStartup; }
            set {
                if (_scanOnStartup != value) {
                    _scanOnStartup = value;
                    NotifyOfPropertyChange(() => ScanOnStartup);
                }
            }
        }

        public bool SimplifyProjectTree {
            get { return _simplifyProjectTree; }
            set {
                if (_simplifyProjectTree != value) {
                    _simplifyProjectTree = value;
                    NotifyOfPropertyChange(() => SimplifyProjectTree);
                }
            }
        }

        public bool IncludeReferencedProjects {
            get { return _includeReferencedProjects; }
            set {
                if (_includeReferencedProjects != value) {
                    _includeReferencedProjects = value;
                    NotifyOfPropertyChange(() => IncludeReferencedProjects);
                }
            }
        }

        public string ReferenceFolderName {
            get { return _referenceFolderName; }
            set {
                if (_referenceFolderName != value) {
                    _referenceFolderName = value;
                    NotifyOfPropertyChange(() => ReferenceFolderName);
                }
            }
        }

        public int ReferenceTreeDepth {
            get { return _referenceTreeDepth; }
            set {
                if (_referenceTreeDepth != value) {
                    _referenceTreeDepth = value;
                    NotifyOfPropertyChange(() => ReferenceTreeDepth);
                }
            }
        }

        public bool IsFlatMode {
            get { return _isFlatMode; }
            set {
                if (_isFlatMode != value) {
                    _isFlatMode = value;
                    NotifyOfPropertyChange(() => IsFlatMode);
                }
            }
        }

        public VisualStudioVersion VisualStudioVersion {
            get { return _visualStudioVersion; }
            set {
                if (_visualStudioVersion != value) {
                    _visualStudioVersion = value;
                    NotifyOfPropertyChange(() => VisualStudioVersion);
                }
            }
        }

        public override void NotifyOfPropertyChange(string propertyName) {
            base.NotifyOfPropertyChange(propertyName);

            // if any property changed that is not CanOk, we want the UI to evaluate that property
            Expression<Func<bool>> property = () => CanOk;
            var canOkName = property.GetMemberInfo().Name;
            if (propertyName != canOkName) {
                NotifyOfPropertyChange(property);
            }
        }

        public void Ok() {
            _settings.ScanOnStartup = ScanOnStartup;
            _settings.SimplifyProjectTree = SimplifyProjectTree;
            _settings.IncludeReferencedProjects = IncludeReferencedProjects;
            _settings.ReferenceFolderName = ReferenceFolderName;
            _settings.ReferenceTreeDepth = ReferenceTreeDepth;
            _settings.IsFlatMode = IsFlatMode;
            _settings.VisualStudioVersion = VisualStudioVersion;

            TryClose(true);
        }

        public bool CanOk {
            get {
                return
                    ScanOnStartup != _settings.ScanOnStartup ||
                    SimplifyProjectTree != _settings.SimplifyProjectTree ||
                    IncludeReferencedProjects != _settings.IncludeReferencedProjects ||
                    ReferenceFolderName != _settings.ReferenceFolderName ||
                    ReferenceTreeDepth != _settings.ReferenceTreeDepth ||
                    IsFlatMode != _settings.IsFlatMode ||
                    VisualStudioVersion != _settings.VisualStudioVersion;
            }
        }

        public void Cancel() {
            TryClose(false);
        }
    }
}