﻿using DevelopXamarinTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopXamarinTest.InfraStructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
