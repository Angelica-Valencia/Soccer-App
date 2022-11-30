using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Soccer_App.Model;
using Soccer_App.Service;
using Xamarin.CommunityToolkit;
using System.Linq;
using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Xamarin.CommunityToolkit.Core;
using System.ComponentModel;
using Xamarin.Essentials;

namespace Soccer_App.ViewModel
{
	public class MyCollection
	{
        #region Singleton Pattern
        private MyCollection()
		{

		}
        public static MyCollection Instance { get; } = new MyCollection();
        #endregion
        private IList<Datum> _dtos;
        public IList<Datum> MyObservableCollection
        {
            get { return _dtos; }
            set { _dtos = value; }
        }
    }
}

