using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivium.Models;

namespace Trivium.ViewModels
{
    public class EncryptionViewModel : ViewModel
    {
        private CryptoKey _key;
        private string _text;

        public Dictionary<BinaryLength, string> BinaryLengthLabels { get; set; }

        public BinaryLength KeyLength
        {
            get
            {
                return (BinaryLength)_key.Length;
            }
            set
            {
                _key.Length = (int)value;
                OnPropertyChanged("KeyLength");
            }
        }

        public string KeyVaue
        {
            get { return _key.Value; }
            set
            {
                if (IsNewValue(value, _key.Value))
                {
                    _key.Value = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (IsNewValue(value, _text))
                    SetField(ref _text, value, "Text");
            }
        }

        public EncryptionViewModel()
        {
            _key = new CryptoKey();
            _text = string.Empty;
            BinaryLengthLabels = new()
            {
                { BinaryLength.bit64, "64 bits"},
                { BinaryLength.bit128, "128 bits"},
            };
        }
    }
}