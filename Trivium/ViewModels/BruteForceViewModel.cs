using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Trivium.Models;

namespace Trivium.ViewModels
{
    public class BruteForceViewModel : ViewModel
    {
        public EncryptionViewModel EncryptionViewModel;

        private BruteForce bruteForce;

        public BruteForce BruteForce
        {
            get
            {
                if (bruteForce is null)
                    bruteForce = CreateBrutForce();
                return bruteForce;
            }
        }

        public BruteForce CreateBrutForce()
        {
            var decryptor = new Decryptor(EncryptionViewModel.Encryptor);
            var bruteForce = new BruteForce(EncryptionViewModel.Text, EncryptionViewModel.EncryptedText, decryptor);
            return bruteForce;
        }

        private string _timeInfo;

        public string TimeInfo
        {
            get { return _timeInfo; }
            set { SetField(ref _timeInfo, value); }
        }

        private string percent;

        public string Percent
        {
            get { return percent; }
            set { SetField(ref percent, value); }
        }

        public BruteForceViewModel()
        {
        }

        public BruteForceViewModel(EncryptionViewModel encryptionViewModel)
        {
            this.EncryptionViewModel = encryptionViewModel;
            AttackLogs = new ObservableCollection<AttackResult>();
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        private ObservableCollection<AttackResult> _attackLogs;

        public ObservableCollection<AttackResult> AttackLogs
        {
            get { return _attackLogs; }
            set { SetField(ref _attackLogs, value); }
        }

        private Dispatcher dispatcher;

        public IEnumerable<AttackResult> Atack()
        {
            foreach (var result in this.BruteForce.Atack())
            {
                AddToGrid(result);
                yield return result;
            }
            var time = BruteForce.EndAttackTime - BruteForce.StartAttackTime;
            var totalTime = time.TotalMinutes.ToString("0.00");
            TimeInfo = $"{bruteForce.LastId} keys / {totalTime} min";
            this.Percent = bruteForce.Percent;
        }

        public IEnumerable<AttackResult> AttackClickMock()
        {
            var id = 1;
            while (id < 10)
            {
                Task.Delay(200).Wait();
                var attackResult = new AttackResult(id.ToString(), "mock");
                AddToGrid(attackResult);
                yield return attackResult;
                id++;
            }
        }

        private void AddToGrid(AttackResult attackResult)
        {
            dispatcher.BeginInvoke(() =>
            {
                this.AttackLogs.Add(attackResult);
            });
        }
    }
}