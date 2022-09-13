﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mids_Reborn.Core.Base.Display;

namespace Mids_Reborn.Controls
{
    public partial class ListLabelV3 : UserControl
    {
        public delegate void EmptyHoverEventHandler();
        public delegate void ExpandChangedEventHandler(bool Expanded);
        public delegate void ItemClickEventHandler(ListLabelItemV3 Item, MouseButtons Button);
        public delegate void ItemHoverEventHandler(ListLabelItemV3 Item);
        public delegate void ItemHoverEventHandler2(object sender, ListLabelItemV3 Item);

        public enum LLFontFlags
        {
            Normal = 0,
            Bold = 1,
            Italic = 2,
            Underline = 4,
            Strikethrough = 8
        }

        public enum LLItemState
        {
            Enabled,
            Selected,
            Disabled,
            SelectedDisabled,
            Invalid,
            Heading
        }

        public enum LLTextAlign
        {
            Left,
            Center,
            Right
        }

        private readonly Color[] Colors;
        private readonly Cursor[] Cursors;
        private readonly bool[] HighlightOn;
        private Color bgColor;
        private Color hvrColor;

        private ExtendedBitmap bxBuffer;

        private bool canExpand;
        private bool canScroll;
        private bool DisableEvents;
        private bool DisableRedraw;
        private bool DragMode;
        private int expandMaxY;

        public ListLabelItemV3[] Items => _Items.ToArray();
        private List<ListLabelItemV3> _Items;

        private eMouseTarget LastMouseMoveTarget;
        private Color scBarColor;
        private Color scButtonColor;
        private int ScrollOffset;
        private int ScrollSteps;
        private int ScrollWidth;
        private Size szNormal;
        private Rectangle TextArea;
        private bool TextOutline;
        private int VisibleLineCount;

        private int xPadding;
        private int yPadding;

        public ListLabelV3()
        {
            MouseLeave += ListLabelV3_MouseLeave;
            MouseMove += ListLabelV3_MouseMove;
            MouseMove += ListLabelV3_MouseMove2;
            MouseUp += ListLabelV3_MouseUp;
            Paint += ListLabelV3_Paint;
            Resize += ListLabelV3_Resize;
            FontChanged += ListLabelV3_FontChanged;
            Load += ListLabelV3_Load;
            MouseDown += ListLabelV3_MouseDown;
            MouseWheel += ListLabelV3_MouseWheel;
            bxBuffer = null;
            _Items = new List<ListLabelItemV3>();
            Colors = new[]
            {
                Color.LightBlue, Color.LightGreen, Color.LightGray, Color.DarkGreen, Color.Red, Color.Orange
            };
            Cursors = new[]
            {
                System.Windows.Forms.Cursors.Hand, System.Windows.Forms.Cursors.Hand,
                System.Windows.Forms.Cursors.Default,
                System.Windows.Forms.Cursors.Hand, System.Windows.Forms.Cursors.Hand,
                System.Windows.Forms.Cursors.Default
            };
            HighlightOn = new[]
            {
                true, true, true, true, true, true, false
            };
            bgColor = Color.Black;
            hvrColor = Color.WhiteSmoke;
            TextOutline = true;
            xPadding = 1;
            yPadding = 1;
            ActualLineHeight = 8;
            HoverID = -1;
            DisableRedraw = true;
            DisableEvents = false;
            canScroll = true;
            ScrollOffset = 0;
            canExpand = false;
            isExpanded = false;
            szNormal = Size;
            expandMaxY = 400;
            TextArea = new Rectangle(0, 0, Width, Height);
            ScrollWidth = 8;
            scBarColor = Color.FromArgb(128, 96, 192);
            scButtonColor = Color.FromArgb(96, 0, 192);
            ScrollSteps = 0;
            DragMode = false;
            LastMouseMoveTarget = eMouseTarget.None;
            VisibleLineCount = 0;

            InitializeComponent();
        }

        public bool isExpanded { get; set; }

        public Color TextColor
        {
            get
            {
                Color result;
                if ((Item.ItemState < LLItemState.Enabled) | (Item.ItemState > LLItemState.Heading))
                    result = Color.Black;
                else
                    result = Colors[(int) Item.ItemState];

                return result;
            }
            set
            {
                if ((Item.ItemState < LLItemState.Enabled) | (Item.ItemState > LLItemState.Heading))
                    return;
                Colors[(int) Item.ItemState] = value;
                Draw();
            }
        }

        public override Color BackColor
        {
            get => bgColor;
            set
            {
                bgColor = value;
                Draw();
            }
        }

        public Color HoverColor
        {
            get => hvrColor;
            set
            {
                hvrColor = value;
                Draw();
            }
        }

        public int PaddingX
        {
            get => xPadding;
            set
            {
                if (!((value >= 0) & checked(value * 2 < Width - 5)))
                    return;
                xPadding = value;
                Draw();
            }
        }

        public int PaddingY
        {
            get => yPadding;
            set
            {
                if (!((value >= 0) & (value < Height / 3.0)))
                    return;
                yPadding = value;
                SetLineHeight();
                Draw();
            }
        }

        public bool HighVis
        {
            get => TextOutline;
            set
            {
                TextOutline = value;
                Draw();
            }
        }

        private int HoverID { get; set; }

        public bool SuspendRedraw
        {
            get => DisableRedraw;
            set
            {
                DisableRedraw = value;
                if (value)
                    return;
                if (isExpanded) RecomputeExpand();

                if (isExpanded)
                    return;
                Recalculate();
                Draw();
            }
        }

        public bool Scrollable
        {
            get => canScroll;
            set
            {
                canScroll = value;
                Draw();
            }
        }

        public bool Expandable
        {
            get => canExpand;
            set
            {
                canExpand = value;
                Draw();
            }
        }

        public Size SizeNormal
        {
            get => szNormal;
            set
            {
                szNormal = value;
                Expand(isExpanded);
                Draw();
            }
        }

        public int MaxHeight
        {
            get => expandMaxY;
            set
            {
                if (value < szNormal.Height)
                    return;
                if (value > 2000)
                    return;
                expandMaxY = value;
                Draw();
            }
        }

        public int ScrollBarWidth
        {
            get => ScrollWidth;
            set
            {
                if ((value > 0) & (value < Width / 2.0)) ScrollWidth = value;

                Recalculate();
                Draw();
            }
        }

        public Color ScrollBarColor
        {
            get => scBarColor;
            set
            {
                scBarColor = value;
                Draw();
            }
        }

        public Color ScrollButtonColor
        {
            get => scButtonColor;
            set
            {
                scButtonColor = value;
                Draw();
            }
        }

        private ListLabelItemV3 Item
        {
            get
            {
                if ((Item.Index < 0) | (Item.Index > checked(_Items.Count - 1)))
                    return new ListLabelItemV3();
                return _Items[Item.Index];
            }
            set
            {
                if ((Item.Index < 0) | (Item.Index > checked(_Items.Count - 1)))
                    return;
                _Items[Item.Index] = new ListLabelItemV3(value);
                Draw();
            }
        }

        public int ContentHeight => Height;

        public int DesiredHeight => checked(GetTotalLineCount() * ActualLineHeight);

        public int ActualLineHeight { get; set; }

        public event ItemClickEventHandler ItemClick;
        public event ItemHoverEventHandler ItemHover;
        public event ItemHoverEventHandler2 ItemHover2;
        public event EmptyHoverEventHandler EmptyHover;
        public event ExpandChangedEventHandler ExpandChanged;

        private int GetRealTotalHeight()
        {
            return _Items.Sum(e => e.ItemHeight);
        }

        private void FillDefaultItems()
        {
            if (!Debugger.IsAttached && !Process.GetCurrentProcess().ProcessName.ToLowerInvariant().Contains("devenv")) return;
            
            ClearItems();
            AddItem(new ListLabelItemV3("Header Item", LLItemState.Heading, -1, -1, -1, "", LLFontFlags.Bold, LLTextAlign.Center));
            AddItem(new ListLabelItemV3("Enabled", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Disabled Item", LLItemState.Disabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Selected Item", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold | LLFontFlags.Italic));
            AddItem(new ListLabelItemV3("SD Item", LLItemState.SelectedDisabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Invalid Item", LLItemState.Invalid, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Automatic multiline Item", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Scrollable", LLItemState.Heading, -1, -1, -1, "", LLFontFlags.Bold, LLTextAlign.Center));
            AddItem(new ListLabelItemV3("Item 1", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 2", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 3", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 4", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 5", LLItemState.Disabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 6", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 7", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 8", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 9", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 10", LLItemState.Disabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 11", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 12", LLItemState.Selected, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 13", LLItemState.Invalid, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 14", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            AddItem(new ListLabelItemV3("Item 15", LLItemState.Enabled, -1, -1, -1, "", LLFontFlags.Bold));
            Draw();
        }

        public void AddItem(ListLabelItemV3 iItem)
        {
            DisableEvents = true;
            if (iItem.Index < 0)
            {
                iItem.Index = _Items.Count;
            }

            _Items.Add(iItem);
            WrapString(_Items.Count - 1);
            GetScrollSteps();
            DisableEvents = false;
        }

        public void ClearItems()
        {
            _Items = new List<ListLabelItemV3>();
            ScrollOffset = 0;
            HoverID = -1;
        }

        private void SetLineHeight()
        {
            var font = new Font(Font, Font.Style);
            ActualLineHeight = checked((int) Math.Round(font.GetHeight() + checked(yPadding * 2)));
            VisibleLineCount = GetVisibleLineCount();
        }

        private void Recalculate(bool expanded = false)
        {
            checked
            {
                if (_Items.Count == 0) return;
                InitBuffer();
                if (AutoSize)
                {
                    if (AutoSizeMode == AutoSizeMode.GrowAndShrink)
                    {
                        Height = DesiredHeight;
                    }
                    else if (DesiredHeight > SizeNormal.Height)
                    {
                        Height = DesiredHeight;
                    }
                    else
                    {
                        Height = SizeNormal.Height;
                    }
                }
                else if (Name == "llAncillary" | Name.StartsWith("llPool"))
                {
                    Height = 18 * _Items.Count;
                }
                
                var bRect = new Rectangle(xPadding, 0, Width - xPadding * 2, Height);
                RecalcLines(bRect);
                if ((ScrollSteps > 0) | isExpanded)
                {
                    bRect = new Rectangle(xPadding, 0, Width - xPadding * 2, Height - (ScrollWidth + yPadding));
                    RecalcLines(bRect);
                }

                if (expanded || ScrollSteps <= 0) return;
                var num = (canExpand) ? ScrollWidth + yPadding : 0;

                bRect = new Rectangle(xPadding, 0, Width - (xPadding * 2 + ScrollWidth), Height - num);
                RecalcLines(bRect);
            }
        }

        private void RecalcLines(Rectangle bRect)
        {
            TextArea = bRect;
            SetLineHeight();
            checked
            {
                for (var i = 0; i < _Items.Count; i++) WrapString(i);

                GetTotalLineCount();
                GetScrollSteps();
            }
        }

        private void WrapString(int Index)
        {
            checked
            {
                if (Operators.CompareString(_Items[Index].Text, "", false) == 0) return;
                InitBuffer();
                var num = 1;
                if (_Items[Index].Text.Contains(" "))
                {
                    var array = _Items[Index].Text.Split(" ".ToCharArray());
                    var stringFormat = new StringFormat(StringFormatFlags.NoWrap);
                    var font = new Font(Font, (FontStyle) _Items[Index].FontFlags);
                    var str = (_Items[Index].ItemState == LLItemState.Heading) ? "~  ~" : "";

                    var text = array[0];
                    for (var i = 1; i < array.Length; i++)
                    {
                        var graphics = bxBuffer.Graphics;
                        var text2 = $"{text} {array[i]}{str}";
                        var font2 = font;
                        var layoutArea = new SizeF(1024f, Height);
                        if (Math.Ceiling(graphics.MeasureString(text2, font2, layoutArea, stringFormat).Width) > TextArea.Width)
                        {
                            text = _Items[Index].ItemState == LLItemState.Heading ? $"{text} ~\r\n~ {array[i]}" : $"{text}\r\n {array[i]}";
                            num++;
                        }
                        else
                        {
                            text = $"{text} {array[i]}";
                        }
                    }

                    _Items[Index].WrappedText = text;
                }
                else
                {
                    _Items[Index].WrappedText = _Items[Index].Text;
                }

                if (_Items[Index].ItemState == LLItemState.Heading)
                {
                    _Items[Index].WrappedText = $"~ {_Items[Index].WrappedText} ~";
                }

                _Items[Index].LineCount = num;
                _Items[Index].ItemHeight = num * (ActualLineHeight - yPadding * 2) + yPadding * 2;
            }
        }

        private void InitBuffer()
        {
            if (DisableRedraw) return;
            if (((Width == 0) | (Height == 0)) & (bxBuffer == null)) return;
            bxBuffer ??= new ExtendedBitmap(Width, Height);

            if ((bxBuffer.Size.Width != Width) | (bxBuffer.Size.Height != Height))
            {
                bxBuffer.Dispose();
                bxBuffer = null;
                GC.Collect();
                if ((Height == 0) | (Width == 0)) return;

                bxBuffer = new ExtendedBitmap(Width, Height);
            }

            bxBuffer.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            bxBuffer.Graphics.TextContrast = 0;
            bxBuffer.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            bxBuffer.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            bxBuffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private int GetItemAtY(int y)
        {
            var num = 0;
            checked
            {
                int result;
                if (y > Height)
                {
                    result = -1;
                }
                else
                {
                    for (var i = ScrollOffset; i < _Items.Count; i++)
                    {
                        num += _Items[i].ItemHeight;
                        if (y < num) return i;
                    }

                    result = -1;
                }

                return result;
            }
        }

        private eMouseTarget GetMouseTarget(int x, int y)
        {
            checked
            {
                eMouseTarget result;
                if ((x >= TextArea.Left) & (x <= TextArea.Right) & (y <= TextArea.Bottom))
                {
                    result = eMouseTarget.Item;
                }
                else if ((x >= TextArea.Left) & (x <= TextArea.Right) & (y > TextArea.Bottom))
                {
                    result = eMouseTarget.ExpandButton;
                }
                else if ((x > TextArea.Right) & (y <= ScrollWidth + yPadding))
                {
                    result = eMouseTarget.UpButton;
                }
                else if ((x > TextArea.Right) & (y >= Height - (ScrollWidth + yPadding)))
                {
                    result = eMouseTarget.DownButton;
                }
                else if (!((x > TextArea.Right) & (ScrollSteps > 0)))
                {
                    result = eMouseTarget.None;
                }
                else
                {
                    var num = Height - (yPadding + ScrollWidth) * 2 - yPadding * 2;
                    var num2 = (int) Math.Round(checked(ScrollWidth + yPadding * 2) +
                                                num / (double) ScrollSteps * ScrollOffset);
                    var num3 = (int) Math.Round(num / (double) ScrollSteps);
                    if (y < num2)
                        result = eMouseTarget.ScrollBarUp;
                    else if (y > num2 + num3)
                        result = eMouseTarget.ScrollBarDown;
                    else
                        result = eMouseTarget.ScrollBlock;
                }

                return result;
            }
        }

        private void ListLabelV3_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((e.Delta > 0) & (ScrollSteps > 0) & (ScrollOffset > 0))
            {
                ScrollOffset--;
                Draw();
            }
            else if ((e.Delta < 0) & (ScrollOffset + 1 < ScrollSteps))
            {
                ScrollOffset++;
                Draw();
            }
        }

        private void ListLabelV3_MouseDown(object sender, MouseEventArgs e)
        {
            checked
            {
                if ((e.Button == MouseButtons.Left) & (ModifierKeys == (Keys.Shift | Keys.Control | Keys.Alt)))
                {
                    DisableEvents = false;
                    DisableRedraw = false;
                    Recalculate();
                    Draw();
                    var graphics = CreateGraphics();
                    var powderBlue = Pens.PowderBlue;
                    var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                    graphics.DrawRectangle(powderBlue, rect);
                }
                else if (!DisableEvents)
                {
                    int num;
                    if (ScrollSteps / 3.0 < 5.0)
                        num = (int) Math.Round(ScrollSteps / 3.0);
                    else
                        num = 5;

                    switch (GetMouseTarget(e.X, e.Y))
                    {
                        case eMouseTarget.Item:
                        {
                            var itemAtY = GetItemAtY(e.Y);
                            if (itemAtY > -1)
                            {
                                var num2 = 0;
                                for (var i = ScrollOffset; i < itemAtY; i++) num2 += _Items[i].ItemHeight;

                                if (((num2 + _Items[itemAtY].ItemHeight >= e.Y) &
                                     (num2 + _Items[itemAtY].ItemHeight <= TextArea.Height)) |
                                    ((_Items[itemAtY].LineCount > 1) & (num2 + ActualLineHeight >= e.Y) &
                                     (num2 + ActualLineHeight <= TextArea.Height)))
                                    ItemClick?.Invoke(_Items[itemAtY], e.Button);
                            }

                            break;
                        }
                        case eMouseTarget.UpButton:
                            if ((ScrollSteps > 0) & (ScrollOffset > 0))
                            {
                                ScrollOffset--;
                                Draw();
                            }

                            break;
                        case eMouseTarget.DownButton:
                            if ((ScrollSteps > 0) & (ScrollOffset + 1 < ScrollSteps))
                            {
                                ScrollOffset++;
                                Draw();
                            }

                            break;
                        case eMouseTarget.ScrollBarUp:
                            if (ScrollSteps > 0)
                            {
                                ScrollOffset -= num;
                                if (ScrollOffset < 0) ScrollOffset = 0;

                                Draw();
                            }

                            break;
                        case eMouseTarget.ScrollBarDown:
                            if (ScrollSteps > 0)
                            {
                                ScrollOffset += num;
                                if (ScrollOffset >= ScrollSteps) ScrollOffset = ScrollSteps - 1;

                                Draw();
                            }

                            break;
                        case eMouseTarget.ScrollBlock:
                            if (ScrollSteps > 0) DragMode = true;

                            break;
                        case eMouseTarget.ExpandButton:
                        {
                            if (!isExpanded)
                            {
                                isExpanded = true;
                                ScrollOffset = 0;
                                RecomputeExpand();
                            }
                            else
                            {
                                DisableRedraw = true;
                                Height = szNormal.Height;
                                isExpanded = false;
                                Recalculate();
                                DisableRedraw = false;
                                Draw();
                            }

                            ExpandChanged?.Invoke(isExpanded);
                            break;
                        }
                    }
                }
            }
        }

        private void Expand(bool State)
        {
            if (State)
            {
                isExpanded = true;
                ScrollOffset = 0;
                RecomputeExpand();
            }
            else
            {
                DisableRedraw = true;
                Height = szNormal.Height;
                isExpanded = false;
                Recalculate();
                DisableRedraw = false;
                Draw();
            }

            ExpandChanged?.Invoke(isExpanded);
        }

        private void RecomputeExpand()
        {
            if (!isExpanded) return;
            
            var num = expandMaxY;
            Recalculate(true);
            var num2 = checked(GetRealTotalHeight() + ScrollWidth + yPadding * 3);
            if (num2 < num) num = num2;

            DisableRedraw = true;
            Height = num;
            Recalculate();
            DisableRedraw = false;
            Draw();
        }

        private void ListLabelV3_MouseLeave(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Default;
            LastMouseMoveTarget = eMouseTarget.None;
            HoverID = -1;
            Draw();
            EmptyHover?.Invoke();
        }

        private void ListLabelV3_MouseMove2(object sender, MouseEventArgs e)
        {
            var cursor = System.Windows.Forms.Cursors.Default;
            var mouseTarget = GetMouseTarget(e.X, e.Y);
            var flag = false;
            checked
            {
                if (!DragMode)
                {
                    EmptyHoverEventHandler emptyHoverEvent;
                    switch (mouseTarget)
                    {
                        case eMouseTarget.Item:
                            {
                                var itemAtY = GetItemAtY(e.Y);
                                if (itemAtY <= -1)
                                {
                                    if (HoverID != -1) flag = true;

                                    HoverID = -1;
                                    EmptyHover?.Invoke();
                                    goto IL_3EA;
                                }

                                var num = 0;
                                for (var i = ScrollOffset; i < itemAtY; i++) num += _Items[i].ItemHeight;

                                if (!(((num + _Items[itemAtY].ItemHeight >= e.Y) &
                                       (num + _Items[itemAtY].ItemHeight <= TextArea.Height)) |
                                      ((_Items[itemAtY].LineCount > 1) & (num + ActualLineHeight >= e.Y) &
                                       (num + ActualLineHeight <= TextArea.Height))))
                                {
                                    if (HoverID != -1) flag = true;

                                    HoverID = -1;
                                    EmptyHover?.Invoke();
                                    goto IL_3EA;
                                }

                                cursor = Cursors[(int)_Items[itemAtY].ItemState];
                                HoverID = itemAtY;
                                Draw();
                                ItemHover2?.Invoke(sender, _Items[itemAtY]);
                                goto IL_3EA;
                            }
                        case eMouseTarget.UpButton:
                            if (LastMouseMoveTarget != mouseTarget) Draw();

                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                        case eMouseTarget.DownButton:
                            if (LastMouseMoveTarget != mouseTarget) Draw();

                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                        case eMouseTarget.ExpandButton:
                            HoverID = -1;
                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                    }

                    emptyHoverEvent = EmptyHover;
                    emptyHoverEvent?.Invoke();
                }
                else if (e.Button == MouseButtons.None)
                {
                    DragMode = false;
                }
                else
                {
                    cursor = System.Windows.Forms.Cursors.SizeNS;
                    var num3 = Height - (yPadding + ScrollWidth) * 2 - yPadding * 2;
                    var num4 = (int)Math.Round(checked(ScrollWidth + yPadding * 2) +
                                                num3 / (double)ScrollSteps * ScrollOffset);
                    var num5 = (int)Math.Round(num3 / (double)ScrollSteps);
                    if ((e.Y < num4) & (ScrollOffset > 0))
                    {
                        ScrollOffset--;
                        Draw();
                    }
                    else if ((e.Y > num4 + num5) & (ScrollOffset + 1 < ScrollSteps))
                    {
                        ScrollOffset++;
                        Draw();
                    }

                    var emptyHoverEvent = EmptyHover;
                    emptyHoverEvent?.Invoke();
                }

                IL_3EA:
                if (flag) Draw();

                Cursor = cursor;
                LastMouseMoveTarget = mouseTarget;
            }
        }
        private void ListLabelV3_MouseMove(object sender, MouseEventArgs e)
        {
            var cursor = System.Windows.Forms.Cursors.Default;
            var mouseTarget = GetMouseTarget(e.X, e.Y);
            var flag = false;
            checked
            {
                if (!DragMode)
                {
                    EmptyHoverEventHandler emptyHoverEvent;
                    switch (mouseTarget)
                    {
                        case eMouseTarget.Item:
                        {
                            var itemAtY = GetItemAtY(e.Y);
                            if (itemAtY <= -1)
                            {
                                if (HoverID != -1) flag = true;

                                HoverID = -1;
                                EmptyHover?.Invoke();
                                goto IL_3EA;
                            }

                            var num = 0;
                            for (var i = ScrollOffset; i < itemAtY ; i++) num += _Items[i].ItemHeight;

                            if (!(((num + _Items[itemAtY].ItemHeight >= e.Y) &
                                   (num + _Items[itemAtY].ItemHeight <= TextArea.Height)) |
                                  ((_Items[itemAtY].LineCount > 1) & (num + ActualLineHeight >= e.Y) &
                                   (num + ActualLineHeight <= TextArea.Height))))
                            {
                                if (HoverID != -1) flag = true;

                                HoverID = -1;
                                EmptyHover?.Invoke();
                                goto IL_3EA;
                            }

                            cursor = Cursors[(int) _Items[itemAtY].ItemState];
                            HoverID = itemAtY;
                            Draw();
                            ItemHover?.Invoke(_Items[itemAtY]);
                            goto IL_3EA;
                        }
                        case eMouseTarget.UpButton:
                            if (LastMouseMoveTarget != mouseTarget) Draw();

                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                        case eMouseTarget.DownButton:
                            if (LastMouseMoveTarget != mouseTarget) Draw();

                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                        case eMouseTarget.ExpandButton:
                            HoverID = -1;
                            emptyHoverEvent = EmptyHover;
                            emptyHoverEvent?.Invoke();
                            goto IL_3EA;
                    }

                    emptyHoverEvent = EmptyHover;
                    emptyHoverEvent?.Invoke();
                }
                else if (e.Button == MouseButtons.None)
                {
                    DragMode = false;
                }
                else
                {
                    cursor = System.Windows.Forms.Cursors.SizeNS;
                    var num3 = Height - (yPadding + ScrollWidth) * 2 - yPadding * 2;
                    var num4 = (int) Math.Round(checked(ScrollWidth + yPadding * 2) +
                                                num3 / (double) ScrollSteps * ScrollOffset);
                    var num5 = (int) Math.Round(num3 / (double) ScrollSteps);
                    if ((e.Y < num4) & (ScrollOffset > 0))
                    {
                        ScrollOffset--;
                        Draw();
                    }
                    else if ((e.Y > num4 + num5) & (ScrollOffset + 1 < ScrollSteps))
                    {
                        ScrollOffset++;
                        Draw();
                    }

                    var emptyHoverEvent = EmptyHover;
                    emptyHoverEvent?.Invoke();
                }

                IL_3EA:
                if (flag) Draw();

                Cursor = cursor;
                LastMouseMoveTarget = mouseTarget;
            }
        }

        private void ListLabelV3_MouseUp(object sender, MouseEventArgs e)
        {
            DragMode = false;
            if (Cursor == System.Windows.Forms.Cursors.SizeNS) Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void ListLabelV3_Paint(object sender, PaintEventArgs e)
        {
            if (bxBuffer == null) Draw();

            if (bxBuffer != null)
                e.Graphics.DrawImage(bxBuffer.Bitmap, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void ListLabelV3_Resize(object sender, EventArgs e)
        {
            ScrollOffset = 0;
            Recalculate();
            Draw();
        }

        public void UpdateTextColors(LLItemState State, Color color)
        {
            if ((State < LLItemState.Enabled) | (State > LLItemState.Heading))
                return;
            Colors[(int) State] = color;
            Draw();
        }

        private void ListLabelV3_FontChanged(object sender, EventArgs e)
        {
            Recalculate();
            Draw();
        }

        private void ListLabelV3_Load(object sender, EventArgs e)
        {
            szNormal = Size;
            DisableRedraw = true;
            InitBuffer();
            Recalculate();
            FillDefaultItems();
            DisableRedraw = false;
        }

        private void Draw()
        {
            if (IsDisposed) return;

            checked
            {
                if (DisableRedraw) return;
                if (!Visible) return;
                InitBuffer();
                if ((Width == 0) | (Height == 0)) return;
                bxBuffer.Graphics.Clear(isExpanded ? Color.Black : BackColor);
                for (var i = ScrollOffset; i < _Items.Count; i++) DrawItem(i);

                DrawScrollBar();
                DrawExpandButton();
                var graphics = CreateGraphics();
                graphics.DrawImageUnscaled(bxBuffer.Bitmap, 0, 0);
            }
        }

        private void DrawItem(int Index)
        {
            checked
            {
                if (Index < 0) return;
                if (Index < ScrollOffset) return;
                if (Index > _Items.Count - 1)  return;
                var num = 0;
                for (var i = ScrollOffset; i < Index; i++)
                {
                    num += _Items[i].ItemHeight;
                    if (num > Height) return;
                }

                var height = _Items[Index].ItemHeight;
                if (_Items[Index].LineCount == 1)
                {
                    if (num + _Items[Index].ItemHeight > TextArea.Height) return;
                }
                else if (num + _Items[Index].ItemHeight > TextArea.Height)
                {
                    if (num + ActualLineHeight > TextArea.Height) return;

                    height = ActualLineHeight - yPadding;
                }

                var rectangle = new Rectangle(TextArea.Left, num, TextArea.Width, height);
                var stringFormat = new StringFormat();
                stringFormat.Alignment = _Items[Index].TextAlign switch
                {
                    LLTextAlign.Left => StringAlignment.Near,
                    LLTextAlign.Center => StringAlignment.Center,
                    LLTextAlign.Right => StringAlignment.Far,
                    _ => stringFormat.Alignment
                };

                var fontStyle = FontStyle.Regular;
                if (_Items[Index].Bold) fontStyle++;
                if (_Items[Index].Italic) fontStyle += 2;
                if (_Items[Index].Underline) fontStyle += 4;
                if (_Items[Index].Strikethrough) fontStyle += 8;

                var font = new Font(new FontFamily("Tahoma"), Font.Size, fontStyle);
                if (Index == HoverID && HighlightOn[(int) _Items[Index].ItemState])
                {
                    var brush = new SolidBrush(hvrColor);
                    bxBuffer.Graphics.FillRectangle(brush, rectangle);
                }

                var brush2 = new SolidBrush(Color.Black);
                if (TextOutline)
                {
                    var r = rectangle;
                    r.X--;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.Y--;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.X++;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.X++;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.Y++;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.Y++;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.X--;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                    r.X--;
                    bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, r, stringFormat);
                }

                brush2 = new SolidBrush(Colors[(int) _Items[Index].ItemState]);
                bxBuffer.Graphics.DrawString(_Items[Index].WrappedText, font, brush2, rectangle, stringFormat);

            }
        }

        private int GetVisibleLineCount()
        {
            int result;
            if (!canScroll)
            {
                ScrollSteps = 0;
                result = 0;
            }
            else if (isExpanded)
            {
                result = GetTotalLineCount();
            }
            else
            {
                result = checked((int) Math.Round(Conversion.Int(TextArea.Height / (double) ActualLineHeight)));
            }

            return result;
        }

        private int GetTotalLineCount()
        {
            return _Items.Sum(e => e.LineCount);
        }

        private int GetScrollSteps()
        {
            checked
            {
                if (!canScroll)
                {
                    ScrollSteps = 0;
                    return 0;
                }

                var num = 0;
                var wrapCount = 0;
                foreach (var e in _Items)
                {
                    num += e.LineCount;
                    if (num > VisibleLineCount) wrapCount++;
                }

                // Zed: add an extra scroll step to ensure the last element is always visible
                if (wrapCount > 0) wrapCount++;
                ScrollSteps = (wrapCount <= 1) ? 0 : wrapCount + 1;

                return ScrollSteps;
            }
        }

        private void DrawScrollBar()
        {
            if ((ScrollWidth < 1) | !canScroll | (ScrollSteps < 1))
                return;
            SolidBrush brush;
            var pen = new Pen(scBarColor);
            var pen2 = new Pen(Color.FromArgb(96, 255, 255, 255), 1f);
            var pen3 = new Pen(Color.FromArgb(128, 0, 0, 0), 1f);
            PointF[] array;
            Rectangle rect;
            checked
            {
                bxBuffer.Graphics.DrawLine(pen, (int) Math.Round(TextArea.Right + ScrollWidth / 2.0),
                    yPadding + ScrollWidth,
                    (int) Math.Round(TextArea.Right + ScrollWidth / 2.0), Height - (ScrollWidth + yPadding));
                brush = new SolidBrush(scButtonColor);
                array = new PointF[3];
                if (ScrollSteps > 0)
                {
                    var num = Height - (yPadding + ScrollWidth) * 2 - yPadding * 2;
                    var y = (int) Math.Round(checked(ScrollWidth + yPadding * 2) +
                                             num / (double) ScrollSteps * ScrollOffset);
                    var height = (int) Math.Round(num / (double) ScrollSteps);
                    rect = new Rectangle(TextArea.Right, y, ScrollWidth, height);
                    bxBuffer.Graphics.FillRectangle(brush, rect);
                    bxBuffer.Graphics.DrawLine(pen2, rect.Left, rect.Top, rect.Left, rect.Bottom);
                    bxBuffer.Graphics.DrawLine(pen2, rect.Left + 1, rect.Top, rect.Right, rect.Top);
                    bxBuffer.Graphics.DrawLine(pen3, rect.Right, rect.Top, rect.Right, rect.Bottom);
                    bxBuffer.Graphics.DrawLine(pen3, rect.Left + 1, rect.Bottom, rect.Right - 1, rect.Bottom);
                }

                rect = new Rectangle(TextArea.Right, yPadding + ScrollWidth, ScrollWidth,
                    Height - (ScrollWidth + yPadding) * 2);
                array[0] = new PointF(rect.Left, rect.Top);
                array[1] = new PointF(rect.Right, rect.Top);
            }

            array[2] = new PointF(rect.Left + rect.Width / 2f, yPadding);
            bxBuffer.Graphics.FillPolygon(brush, array);
            bxBuffer.Graphics.DrawLine(pen2, array[0], array[2]);
            bxBuffer.Graphics.DrawLine(pen3, array[0], array[1]);
            array[0] = new PointF(rect.Left, rect.Bottom);
            array[1] = new PointF(rect.Right, rect.Bottom);
            array[2] = new PointF(rect.Left + rect.Width / 2f, checked(Height - yPadding));
            bxBuffer.Graphics.FillPolygon(brush, array);
            bxBuffer.Graphics.DrawLine(pen2, array[0], array[1]);
            bxBuffer.Graphics.DrawLine(pen3, array[2], array[1]);
        }

        private void DrawExpandButton()
        {
            if (!canExpand | (!isExpanded & (ScrollSteps < 1)))
                return;
            var pen = new Pen(scBarColor);
            var pen2 = new Pen(Color.FromArgb(96, 255, 255, 255), 1f);
            var pen3 = new Pen(Color.FromArgb(128, 0, 0, 0), 1f);
            var brush = new SolidBrush(scButtonColor);
            var array = new PointF[3];
            var rectangle = checked(new Rectangle((int) Math.Round(Width / 3.0), Height - (ScrollWidth + yPadding),
                (int) Math.Round(Width / 3.0), ScrollWidth - yPadding));
            if (isExpanded)
            {
                array[0] = new PointF(rectangle.Left, rectangle.Bottom);
                array[1] = new PointF(rectangle.Right, rectangle.Bottom);
                array[2] = new PointF(rectangle.Left + rectangle.Width / 2f, rectangle.Top);
                bxBuffer.Graphics.FillPolygon(brush, array);
                bxBuffer.Graphics.DrawLine(pen2, array[0], array[2]);
                bxBuffer.Graphics.DrawLine(pen3, array[0], array[1]);
                checked
                {
                    bxBuffer.Graphics.DrawLine(pen, 0, 0, 0, Height - 1);
                    bxBuffer.Graphics.DrawLine(pen, 0, Height - 1, Width - 1, Height - 1);
                    bxBuffer.Graphics.DrawLine(pen, Width - 1, Height, Width - 1, 0);
                }
            }
            else
            {
                array[0] = new PointF(rectangle.Left, rectangle.Top);
                array[1] = new PointF(rectangle.Right, rectangle.Top);
                array[2] = new PointF(rectangle.Left + rectangle.Width / 2f, rectangle.Bottom);
                bxBuffer.Graphics.FillPolygon(brush, array);
                bxBuffer.Graphics.DrawLine(pen2, array[0], array[1]);
                bxBuffer.Graphics.DrawLine(pen3, array[2], array[1]);
            }
        }

        private enum eMouseTarget
        {
            None,
            Item,
            UpButton,
            DownButton,
            ScrollBarUp,
            ScrollBarDown,
            ScrollBlock,
            ExpandButton
        }

        public class ListLabelItemV3
        {
            public readonly int IDXPower;
            public readonly int nIDPower;
            public readonly int nIDSet;
            private readonly string sTag;
            public LLFontFlags FontFlags;
            public int Index;
            public int ItemHeight;
            public int LineCount;
            public string WrappedText;

            public ListLabelItemV3()
            {
                Text = "";
                WrappedText = "";
                ItemState = LLItemState.Enabled;
                FontFlags = LLFontFlags.Normal;
                TextAlign = LLTextAlign.Left;
                nIDSet = -1;
                IDXPower = -1;
                nIDPower = -1;
                sTag = "";
                LineCount = 1;
                ItemHeight = 1;
                Index = -1;
            }

            public ListLabelItemV3(string iText, LLItemState iState, int inIDSet = -1, int iIDXPower = -1, int inIDPower = -1, string iStringTag = "", LLFontFlags iFont = LLFontFlags.Normal, LLTextAlign iAlign = LLTextAlign.Left)
            {
                Text = "";
                WrappedText = "";
                ItemState = LLItemState.Enabled;
                FontFlags = LLFontFlags.Normal;
                TextAlign = LLTextAlign.Left;
                nIDSet = -1;
                IDXPower = -1;
                nIDPower = -1;
                sTag = "";
                LineCount = 1;
                ItemHeight = 1;
                Index = -1;
                Text = iText;
                ItemState = iState;
                nIDSet = inIDSet;
                IDXPower = iIDXPower;
                nIDPower = inIDPower;
                sTag = iStringTag;
                FontFlags = iFont;
                TextAlign = iAlign;
            }

            public ListLabelItemV3(ListLabelItemV3 Template)
            {
                Text = "";
                WrappedText = "";
                ItemState = LLItemState.Enabled;
                FontFlags = LLFontFlags.Normal;
                TextAlign = LLTextAlign.Left;
                nIDSet = -1;
                IDXPower = -1;
                nIDPower = -1;
                sTag = "";
                LineCount = 1;
                ItemHeight = 1;
                Index = -1;
                Text = Template.Text;
                ItemState = Template.ItemState;
                FontFlags = Template.FontFlags;
                TextAlign = Template.TextAlign;
                LineCount = Template.LineCount;
                ItemHeight = Template.ItemHeight;
                nIDSet = Template.nIDSet;
                IDXPower = Template.IDXPower;
                nIDPower = Template.nIDPower;
                sTag = Template.sTag;
            }

            public string Text { get; set; }

            public LLItemState ItemState { get; set; }

            public bool Bold
            {
                get => (FontFlags & LLFontFlags.Bold) > LLFontFlags.Normal;
                set
                {
                    checked
                    {
                        if (value)
                        {
                            if ((~FontFlags & LLFontFlags.Bold) > LLFontFlags.Normal) FontFlags++;
                        }
                        else if ((FontFlags & LLFontFlags.Bold) > LLFontFlags.Normal)
                        {
                            FontFlags--;
                        }
                    }
                }
            }

            public bool Italic
            {
                get => (FontFlags & LLFontFlags.Italic) > LLFontFlags.Normal;
                set
                {
                    checked
                    {
                        if (value)
                        {
                            if ((~FontFlags & LLFontFlags.Italic) > LLFontFlags.Normal) FontFlags += 2;
                        }
                        else if ((FontFlags & LLFontFlags.Italic) > LLFontFlags.Normal)
                        {
                            FontFlags -= 2;
                        }
                    }
                }
            }

            public bool Underline
            {
                get => (FontFlags & LLFontFlags.Underline) > LLFontFlags.Normal;
                set
                {
                    checked
                    {
                        if (value)
                        {
                            if ((~FontFlags & LLFontFlags.Underline) > LLFontFlags.Normal) FontFlags += 4;
                        }
                        else if ((FontFlags & LLFontFlags.Underline) > LLFontFlags.Normal)
                        {
                            FontFlags -= 4;
                        }
                    }
                }
            }

            public bool Strikethrough
            {
                get => (FontFlags & LLFontFlags.Strikethrough) > LLFontFlags.Normal;
                set
                {
                    checked
                    {
                        if (value)
                        {
                            if ((~FontFlags & LLFontFlags.Strikethrough) > LLFontFlags.Normal) FontFlags += 8;
                        }
                        else if ((FontFlags & LLFontFlags.Strikethrough) > LLFontFlags.Normal)
                        {
                            FontFlags -= 8;
                        }
                    }
                }
            }

            public LLTextAlign TextAlign { get; set; }
        }
    }
}