using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM
{
    //Класс КОНЕЧНОГО АВТОМАТА НА СТЕКЕ
    public class FSM
    {
        //Делегат для функций
        public delegate void Stack();

        //Массив делегатов (Стек)
        public Stack[] State;

        //Конструктор создает стек размером 1
        public FSM() {
            State = new Stack[1];
        }

        //Выполнение последней команды в стеке
        public void Update() {
            Stack currentStateFunction = GetCurrentState();
            if (currentStateFunction != null)
                currentStateFunction.Invoke();              //Вызов функции из делегата
        }

        //Получение последней команды в массиве (стеке)
        public Stack GetCurrentState()
        {
            return State.Length > 0 ? State[State.Length - 1] : null;
        }

        //Добавление команды в конец массива (Стека)
        public void PushState(Stack state) {
            if (GetCurrentState() != state) {               //Если последняя команда НЕ равна добовляемой. Что бы избежать дублирования повторяющихся функций
                Array.Resize(ref State, State.Length + 1);  //Изменение размера массива (Ссылка на массив, новая длинна)
                State[State.GetUpperBound(0)] = state;      //Присвоение новой команды в последний индекс массива
            }
        }

        //Удаление последней команды из стека и возврат ее
        public Stack popState() {
            if (State.Length == 0)
                return null;
            Stack answer = State[State.GetUpperBound(0)];   //Получение последней команды из стека
            State[State.GetUpperBound(0)] = null;           //Обнуление последней команды
            Array.Resize(ref State, State.Length - 1);      //Изменение размера массива (Уменьшение)
            return answer;                                  //Возврат удаленной команды
        }
    }
}
