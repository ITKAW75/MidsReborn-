﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mids_Reborn.Core.Utils;

namespace Mids_Reborn.Forms.Controls
{
    public partial class PatchMessenger : Form
    {
        private event EventHandler<string>? UpdateText;
        private string? _text;

        public override string Text
        {
            get => _text ?? base.Text;
            set
            {
                _text = value;
                UpdateText?.Invoke(this, value);
            }
        }
        
        public PatchMessenger()
        {
            Load += OnLoad;
            UpdateText += OnUpdateText;
            InitializeComponent();
        }

        private void OnLoad(object? sender, EventArgs e)
        {
            CenterToParent();
        }

        private void OnUpdateText(object? sender, string e)
        {
            label1.Text = e;
            Refresh();
        }

        public void Completed()
        {
            Close();
        }
    }
}
