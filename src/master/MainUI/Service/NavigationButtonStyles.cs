namespace MainUI.Service
{
    /// <summary>
    /// 按钮样式类，用于设置按钮的样式
    /// </summary>
    public static class NavigationButtonStyles
    {
        #region 用于设置导航按钮的样式
        public static readonly ButtonStyle Active = new()
        {
            BackColor = Color.White,
            ForeColor = Color.FromArgb(64, 64, 64)
        };

        public static readonly ButtonStyle Inactive = new()
        {
            BackColor = Color.FromArgb(196, 199, 204),
            ForeColor = Color.White
        };

        public record ButtonStyle
        {
            public Color BackColor { get; init; }
            public Color ForeColor { get; init; }
        }

        public static void UpdateNavigationButtons(int selectedIndex, 
            ControlMappings controls)
        {
            foreach (var (index, button) in controls.NavigationButtons)
            {
                var style = index == selectedIndex? Active: Inactive;
                button.BackColor = style.BackColor;
                button.ForeColor = style.ForeColor;
            }
        }
        #endregion


        #region 手动控制按钮颜色改变方法
        /// <summary>
        /// 手动控制中设置按钮颜色的静态类
        /// </summary>
        private static class ButtonColors
        {
            public static readonly Color ActiveColor = Color.LimeGreen;
            public static readonly Color InactiveColor = Color.FromArgb(80, 160, 255);
        }

        /// <summary>
        /// 手动控制中设置按钮颜色的方法
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="isActive"></param>
        public static void BtnColor(UIButton btn, bool isActive)
        {
            var color = isActive ? ButtonColors.ActiveColor
                : ButtonColors.InactiveColor;

            // 使用对象初始化器设置所有颜色属性
            var properties = new[]
            {
                nameof(btn.FillColor),
                nameof(btn.RectColor),
                nameof(btn.FillDisableColor),
                nameof(btn.RectDisableColor),
                nameof(btn.FillHoverColor),
                nameof(btn.FillPressColor),
                nameof(btn.FillSelectedColor)
            };

            foreach (var property in properties)
            {
                btn.GetType().GetProperty(property)?.SetValue(btn, color);
            }
        }
        #endregion
    }
}
