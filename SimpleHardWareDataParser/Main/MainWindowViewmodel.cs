using SimpleHardWareDataParser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareDataParser.Main
{
    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        public string TitleName
        {
            get => _titleName;
            set => Set(ref _titleName, value, nameof(TitleName));
        }

        public string RootDirectory
        {
            get => _rootDirectory;
            set => Set(ref _rootDirectory, value, nameof(RootDirectory));
        }



    }

    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        private string _titleName = "Hardware Data Parser";
        private string _rootDirectory = "./Admin/";
    }
}
