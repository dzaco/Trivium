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
        public CryptoKey Key { get; }
        private string _text;

        public Dictionary<BinaryLength, string> BinaryLengthLabels { get; set; }

        public BinaryLength KeyLength
        {
            get
            {
                return (BinaryLength)Key.Length;
            }
            set
            {
                Key.Length = (int)value;
                OnPropertyChanged("KeyLength");
            }
        }

        public string KeyVaue
        {
            get { return Key.Value; }
            set
            {
                if (IsNewValue(value, Key.Value))
                {
                    Key.Value = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        public byte[] Bytes
        {
            get { return Key.Bytes; }
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

        private Encryptor2 encryptor;

        public Encryptor2 Encryptor
        {
            get
            {
                if (encryptor is null)
                    encryptor = new Encryptor2(Key);
                return encryptor;
            }
            set { encryptor = value; }
        }

        private string encryptedText;

        public string EncryptedText
        {
            get { return encryptedText; }
            set
            {
                encryptedText = value;
                OnPropertyChanged();
            }
        }

        public EncryptionViewModel()
        {
            Key = new CryptoKey();
            _text = string.Empty;
            BinaryLengthLabels = new()
            {
                { BinaryLength.bit8, "8 bits"},
                { BinaryLength.bit16, "16 bits"},
                { BinaryLength.bit32, "32 bits"},
                { BinaryLength.bit64, "64 bits"},
                { BinaryLength.bit80, "80 bits"},
                { BinaryLength.bit128, "128 bits"},
            };
        }
    }
}