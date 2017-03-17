﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class UcLine : UserControl
    {
        private const int UNCOVER_LINE_POINT = 1;
        private static int pointSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelNetPoint"));
        private static int pointRadius = pointSize / 2;
        private Action<int> RemoveElement = null;
        private Action<int, Color> ChangeColor = null;
        public int ElementIdx
        {
            get;
            set;
        }
        public ElementLine LineInfo
        {
            get;
            set;
        }

        public UcLine(int element, Point start, Point end, Action<int> removeLine, Action<int, Color> changeColor)
        {
            this.ElementIdx = element;
            this.RemoveElement = removeLine;
            this.ChangeColor = changeColor;

            InitializeComponent();

            if (start.X == end.X)
            {//竖线
                this.Location = new Point(Math.Min(start.X, end.X) + UNCOVER_LINE_POINT, Math.Min(start.Y, end.Y) + pointRadius);
                this.Width = 3;
                this.Height = Math.Abs(start.Y - end.Y);
            }
            else
            {//横线
                this.Location = new Point(Math.Min(start.X, end.X) + pointRadius, Math.Min(start.Y, end.Y) + UNCOVER_LINE_POINT);
                this.Height = 3;
                this.Width = Math.Abs(start.X - end.X);
            }
            this.BackColor = Color.Black;
        }

        public UcLine(ElementLine info, Action<int> removeLine, Action<int, Color> changeColor)
        {
            this.LineInfo = info;
            this.RemoveElement = removeLine;
            this.ChangeColor = changeColor;

            InitializeComponent();

            if (info.LocOtherX == info.LocX)
            {
                this.Height = Math.Abs(info.LocY - info.LocOtherY);
                this.Width = Constants.LINE_LINK_WIDTH;
            }
            else
            {
                this.Width = Math.Abs(info.LocX - info.LocOtherX);
                this.Height = Constants.LINE_LINK_WIDTH;
            }
            this.Location = new Point(Math.Min(info.LocX, info.LocOtherX), Math.Min(info.LocY, info.LocOtherY));
            this.BackColor = this.LineInfo.Color;
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveElement(this.ElementIdx);
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color lineColor = dialog.Color;
                this.BackColor = lineColor;
                this.ChangeColor(this.ElementIdx, lineColor);
                //this.ChangeColor(this.OtherLine.LineInfo.Idx, lineColor.ToArgb());
            }
        }
    }
}