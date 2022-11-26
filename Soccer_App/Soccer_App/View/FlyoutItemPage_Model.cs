using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Soccer_App.View
{
    public class FlyoutItemPage_Model
    {

        //These are the details I want to see  in my FlyoutPage.
        public string Title { get; set; }
        public string IconSource { get; set; }

        //This the field that stores the pages I want to go to.
        public  Type TargetPage { get; set; }

    }
}

