using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MileStone3.PresentationLayer
{
    public class ActionListener : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _startNickname = "";
        public string StartNickname
        {
            get
            {
                return _startNickname;
            }
            set
            {
                _startNickname = value;
                OnPropertyChanged("StartNickname");
            }
        }

        private Boolean _nicknameFilterEnabled = false;
        public Boolean NicknameFilterEnabled
        {
            get
            {
                return _nicknameFilterEnabled;
            }
            set
            {
                _nicknameFilterEnabled = value;
                OnPropertyChanged("NicknameFilterEnabled");
            }
        }

        private Boolean _groupIDFilterEnabled = false;
        public Boolean GroupIDFilterEnabled
        {
            get
            {
                return _groupIDFilterEnabled;
            }
            set
            {
                _groupIDFilterEnabled = value;
                OnPropertyChanged("GroupIDFilterEnabled");
            }
        }

        private String _startGroupId = "";
        public String StartGroupId
        {
            get
            {
                return _startGroupId;
            }
            set
            {
                _startGroupId = value;
                OnPropertyChanged("StartGroupId");
            }
        }


        private string _msgBody = "";
        public string MsgBody
        {
            get
            {
                return _msgBody;
            }
            set
            {
                _msgBody = value;
                OnPropertyChanged("MsgBody");
            }
        }

        // Bind the message display in the chat
        public ObservableCollection<string> DisplayedMsgs { get; } = new ObservableCollection<string>();

        private string _filterNickname = "";
        public string FilterNickname
        {
            get
            {
                return _filterNickname;
            }
            set
            {
                _filterNickname = value;
                OnPropertyChanged("FilterNickname");
            }
        }

        private string _filterGroupId = "";
        public string FilterGroupId
        {
            get
            {
                return _filterGroupId;
            }
            set
            {
                _filterGroupId = value;
                OnPropertyChanged("FilterGroupId");
            }
        }

        private bool[] _sorterMode = new bool[] { true, false };
        public bool[] SorterMode
        {
            get { return _sorterMode; }
        }
        public Boolean SelectedModeAscending
        {
            get { return _sorterMode[0]; }
        }

        private bool[] _sorterType = new bool[] { true, false, false };
        public bool[] SorterType
        {
            get { return _sorterType; }
        }
        public int SelectedTypeSorterIndex
        {
            get
            {
                for (int i = 0; i < _sorterType.Length; i++)
                {
                    if (_sorterType[i])
                        return i;
                }
                return 0;
            }

        }

        private bool[] _filterType = new bool[] { false, false, true };
        public bool[] FilterType
        {
            get { return _filterType; }
        }
        public int SelectedTypeFilterIndex
        {

            get
            {
                for (int i = 0; i < _filterType.Length; i++)
                {
                    if (_filterType[i])
                        return i;
                }
                return 2;
            }
        }

        private String _selectedMsg = "";
        public String SelectedMsg
        {
            get
            {
                return _selectedMsg;
            }
            set
            {
                if (_selectedMsg != value)
                {
                    _selectedMsg = value;
                    OnPropertyChanged(_selectedMsg);
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}