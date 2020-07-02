using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FIRST_CS
{
    /*
     * Реализовать извлечение квадратного корня из дроби, используя
    метод Ньютона. При невозможности выполнения операции должно быть
    сгенерировано исключение
     */

    public sealed class Fraction : IEquatable<Fraction>, IComparable, IComparable<Fraction>, ICloneable
    {
        BigInteger _numerator;
        BigInteger _denominator;
        int _sign = 1;

        public BigInteger Numerator
        {
            get => _numerator;

            private set => _numerator = value;
        }
        public BigInteger Denominator
        {
            get => _denominator;

            private set => _denominator = value;
        }
        public int Sign
        {
            get => _sign;

            private set => _sign = value;
        }

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }

            if (numerator * denominator < 0)
            {
                this._sign = -1;
            }
            else
            {
                this._sign = 1;
            }
            if (numerator < 0) this._numerator = -numerator; else this._numerator = numerator;
            if (denominator < 0) this._denominator = -denominator; else this._denominator = denominator;

            this.CheckForReduction();

        }

        public static Fraction PowFraction(Fraction toPow, int power)
        {
            Fraction temp = (Fraction)toPow.Clone();
            var one = new Fraction(1, 1);
            if (power == 0)
            {
                return one;
            }
            var absPower = Math.Abs(power);
            for (int i = 0; i < absPower; i++)
            {
                temp = temp * temp;
            }
            if (power < 0)
            {
                return one / temp;
            }
            else return temp;
        }

        private void CheckForReduction()
        {
            var numerator = this._numerator;
            var denominator = this._denominator;
            if (Nod(numerator, denominator) != 0)
            {
                this._numerator /= Nod(numerator, denominator);
                this._denominator /= Nod(numerator, denominator);
            }
        }
        private static BigInteger Nod(BigInteger n, BigInteger d)
        {
            if (n < 0) n = -n;
            if (d < 0) d = -d;
            
            while (d != 0 && n != 0)
            {
                if (n % d > 0)
                {
                    var temp = n;
                    n = d;
                    d = temp % d;
                }
                else break;
            }
            if (d != 0 && n != 0) return d;
            return 0;
        }
        private void CheckSign()
        {
            if (this._numerator * this._denominator < 0)
            {
                this._sign *= -1;
            }
            else
            {
                this._sign *= 1;
            }
            if (this._numerator < 0) this._numerator = -this._numerator;
            if (this._denominator < 0) this._denominator = -this._denominator;

        }
        public bool IsZero()
        {
            if (this._numerator == 0)
            {
                return true;
            }
            return false;
        }


        #region Operators
        public static Fraction Addition(Fraction left, Fraction right)
        {
            Fraction newFraction = new Fraction(left._numerator, left._denominator);
            newFraction._numerator *= left.Sign;
            newFraction._numerator *= right._denominator;
            newFraction._numerator += right._numerator * newFraction._denominator * right.Sign;
            newFraction._denominator *= right._denominator;
            
            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator +(Fraction left, Fraction right)
        {
            return Addition(left, right);
        }

        public static Fraction Subtraction(Fraction left, Fraction right)
        {
            Fraction newFraction = new Fraction(left._numerator, left._denominator);
            newFraction._numerator *= left.Sign;
            newFraction._numerator *= right._denominator;
            newFraction._numerator -= right._numerator * newFraction._denominator * right.Sign;
            newFraction._denominator *= right._denominator;

            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator -(Fraction left, Fraction right)
        {
            return Subtraction(left, right);
        }

        public static Fraction Multiplication(Fraction left, Fraction right)
        {
            Fraction newFraction = new Fraction(left._numerator, left._denominator);
            newFraction._numerator *= left.Sign;

            newFraction._numerator *= right._numerator;
            newFraction._numerator *= right.Sign;
            newFraction._denominator *= right._denominator;


            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator *(Fraction left, Fraction right)
        {
            return Multiplication(left, right);
        }

        public static Fraction Division(Fraction left, Fraction right)
        {
            if (right._numerator == 0)
            {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }

            Fraction newFraction = new Fraction(left._numerator, left._denominator);
            newFraction._numerator *= left.Sign;

            newFraction._numerator *= right._denominator;
            newFraction._numerator *= right.Sign;
            newFraction._denominator *= right._numerator;

            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator /(Fraction left, Fraction right)
        {
            return Division(left, right);
        }

        public static Fraction DivisionByInt(Fraction left, int right)
        {
            if (right == 0)
            {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }

            Fraction newFraction = new Fraction(left._numerator, left._denominator);
            newFraction._numerator *= left.Sign;
            newFraction._denominator *= right;

            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator /(Fraction left, int right)
        {
            return DivisionByInt(left, right);
        }

        public Fraction getSquareRootNewTon(Fraction eps)
        {
            var approxim = new Fraction(1, 1);
            var abs = approxim * approxim - this;
            abs.Sign = 1;
            while (abs > eps)
            {
                
                var div = this / approxim;
                //Console.WriteLine($"{approxim} {div}");
                approxim = (div + approxim) / 2;

                abs = approxim * approxim - this;
                abs.Sign = 1;
            }
            return approxim;
        }
        #endregion

        public double GetRoundedValue()
        {
            double omg_n = (double)this._numerator;
            double omg_d = (double)this._denominator;
            if (this.Sign == 1) return omg_n / omg_d;
            else return -omg_n / omg_d;
        }
        public string ToDecimalFractionString(int figuresAfterDotQuantity)
        {
            double omg_n = (double)this._numerator;
            double omg_d = (double)this._denominator;
            var sb = new StringBuilder();
            var figureStr = (omg_n / omg_d).ToString();
            string sPattern = ".";
            bool whole = true;
            foreach (var c in figureStr)
            {
                if (c == '.')
                {
                    whole = false;

                }
            }
            if (whole)
            {
                return figureStr;
            } else
            {
                var i = 0;
                while (figureStr[i] != '.')
                {
                    sb.Append(figureStr[i]);
                    i++;
                }
                if (figuresAfterDotQuantity != 0)
                {
                    for (var i2 = 0; i2 < figuresAfterDotQuantity + 1; i2++)
                    {
                        sb.Append(figureStr[i]);
                        i++;
                    }
                }
                return sb.ToString();
            }
            
        }

        public override string ToString()
        {
            var temp = "-";
            if (this._sign > 0)
            {
                temp = "";
            }
            if(this._denominator == 1)
            {
                return $"{temp}{this._numerator}";
            }
            return $"{temp}{this._numerator}/{this._denominator}";
        }

        #region Equals
        public bool Equals(Fraction other)
        {
            if (other == null)
                return false;

            if (this._numerator == other._denominator && this._sign == other._sign)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Fraction fractionObj = obj as Fraction;
            if (fractionObj == null)
                return false;
            else
                return Equals(fractionObj);
        }

        public static bool operator ==(Fraction fraction1, Fraction fraction2)
        {
            if (((object)fraction1) == null || ((object)fraction2) == null)
                return Object.Equals(fraction1, fraction2);

            return fraction1.Equals(fraction2);
        }

        public static bool operator !=(Fraction fraction1, Fraction fraction2)
        {
            if (((object)fraction1) == null || ((object)fraction2) == null)
                return !Object.Equals(fraction1, fraction2);

            return !(fraction1.Equals(fraction2));
        }
        #endregion

        #region Compare
        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            Fraction otherFraction = obj as Fraction;
            if (obj is Fraction)
            {
                return CompareTo(otherFraction);
            }
            else
                throw new ArgumentException("Object is not a Fraction");
        }
        public int CompareTo(Fraction obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var thisValue = this._numerator * obj._denominator;
            var otherValue = obj._numerator * this._denominator;
            return thisValue.CompareTo(otherValue);

        }
        

        public static bool operator >(Fraction operand1, Fraction operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(Fraction operand1, Fraction operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(Fraction operand1, Fraction operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(Fraction operand1, Fraction operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }
        #endregion

        #region ICloneable

        public object Clone()
        {
            return new Fraction(this._numerator, this._denominator);
        }

        #endregion
    }

}
