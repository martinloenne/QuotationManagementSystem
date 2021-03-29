using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {
  public class CancelDialogViewModel : BaseViewModel {

    public ICommand CancelCommand { get; set; }
    public ICommand AbortCancalCommand { get; set; }

    public CancelDialogViewModel(Action onClose, Action windowToCloseOnCancel) : base(onClose) {
      CancelCommand = new RelayCommand(o => { windowToCloseOnCancel.Invoke(); OnClose(); }, o => true);
      AbortCancalCommand = new RelayCommand(o => OnClose(), o => true);
    }
  }
}
