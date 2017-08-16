﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Binding;

namespace NewsPicker.Web.Controls
{
    public class EmptyDataTemplate : DotvvmMarkupControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, EmptyDataTemplate>(c => c.Title, "Nothing here yet...");

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, EmptyDataTemplate>(c => c.Description, "");
    }
}