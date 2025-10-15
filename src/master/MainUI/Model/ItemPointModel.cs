using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 项点模型
    /// </summary>
    public class ItemPointModel : NotifyProperty
    {
        int _colorState;
        [DefaultValue(0)]
        /// <summary>
        ///  0:默认 1:绿色 2:黄色 3:红色
        /// </summary>
        public int ColorState
        {
            get => _colorState;
            set => _colorState = value;
        }

        string _itemname;
        /// <summary>
        /// 项点名称
        /// </summary>
        public string ItemName
        {
            get => _itemname;
            set => _itemname = value;
        }

        string _itemkey;
        /// <summary>
        /// 项点键值
        /// </summary>
        public string ItemKey
        {
            get => _itemkey;
            set => _itemkey = value;
        }

        bool _check;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Check
        {
            get => _check;
            set
            {
                if (_check == value) return;
                _check = value;
                OnPropertyChanged(nameof(Check));
            }
        }
    }

   
}
