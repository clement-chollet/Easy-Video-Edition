using System;
using System.Windows.Input;

class RelayCommand : ICommand
{
    /// <summary>
    /// Class that describe a relay Command
    /// </summary>

    private Action function;
    private Predicate<Object> canExec;
    private Action<Object> functionWithParam;

    /// <summary>
    /// Create a relay command who launch the function f
    /// </summary>
    /// <param name="f">Function to use when the command is called</param>
    public RelayCommand(Action f)
    {
        this.function = f;
    }

    /// <summary>
    /// Create a relay command who launch the function f. Can have parameter
    /// </summary>
    /// <param name="f">Function to launch</param>
    public RelayCommand(Action<Object> f)
    {
        this.functionWithParam = f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <param name="canExec"></param>
    public RelayCommand(Action f, Predicate<Object> canExec)
    {
        this.function = f;
        this.canExec = canExec;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object parameter)
    {
        if (this.canExec == null)
            return this.function != null || this.functionWithParam != null;
        else
            return this.canExec(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
        if (this.function != null)
        {
            this.function();
        }
        else
        {
            if (this.functionWithParam != null)
                this.functionWithParam(parameter);
        }
      
    }
}