using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class Money
    {
        private int _value;
        public Money(int value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return (obj as Money)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(money1._value + money2._value);
        }

        public static Money operator -(Money money1, Money money2)
        {
            return new Money(money1._value - money2._value);
        }

        public static bool operator ==(Money money1, Money money2)
        {
            return (money1._value == money2._value);
        }


        public static bool operator !=(Money money1, Money money2)
        {
            return (money1._value != money2._value);
        }

        public static Money operator *(Money money, int iterations)
        {
            return new Money(money._value * iterations);
        }

        public override string ToString()
        {
            return string.Format("{0}", _value);
        }
    }
}
