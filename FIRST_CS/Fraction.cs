using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FIRST_CS
{

    public sealed class Fraction : IEquatable<Fraction>, IComparable, IComparable<Fraction>, ICloneable
    {
        BigInteger _numerator;
        BigInteger _denominator;

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

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }

            this._numerator = numerator;
            this._denominator = denominator;

            this.CheckSign();
            this.CheckForReduction();
        }

        public static Fraction PowFraction(Fraction toPow, int power)
        {
           
            var one = new Fraction(1, 1);
            var res = new Fraction(1, 1);
            if (power == 0)
            {
                return one;
            }
            if (power == 1)
            {
                return (Fraction)toPow.Clone();
            }
            var absPower = Math.Abs(power);
            for (int i = 0; i < absPower; i++)
            {
                res *= toPow;
            }
            if (power < 0)
            {
                return one / res;
            }
            else return res;
        }

        private void CheckSign()
        {
            if (this._denominator < 0 && this._numerator < 0)
            {
                this._numerator = BigInteger.Abs(this._numerator);
                this._denominator = BigInteger.Abs(this._denominator);
            }
            else if (this._denominator < 0 && this._numerator > 0)
            {
                //
            }
            else if (this._denominator > 0 && this._numerator < 0)
            {
                this._numerator = BigInteger.Abs(this._numerator);
                this._denominator = BigInteger.MinusOne * this._denominator;
            }
            else if (this._denominator > 0 && this._numerator > 0)
            {
                //
            }
        }
        private void CheckForReduction()
        {
            var numerator = this._numerator;
            var denominator = this._denominator;
            if (BigInteger.GreatestCommonDivisor(BigInteger.Abs(numerator), BigInteger.Abs(denominator)) != 1)
            {
                this._numerator /= BigInteger.GreatestCommonDivisor(BigInteger.Abs(numerator), BigInteger.Abs(denominator));
                this._denominator /= BigInteger.GreatestCommonDivisor(BigInteger.Abs(numerator), BigInteger.Abs(denominator));
            }
        }


        #region Operators
        public static Fraction Addition(Fraction left, Fraction right)
        {
            Fraction newFraction = new Fraction(left._numerator, left._denominator);


            newFraction._numerator *= right._denominator;
            
            newFraction._numerator += right._numerator * newFraction._denominator;
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
            newFraction._numerator *= right._denominator;
            newFraction._numerator -= right._numerator * newFraction._denominator;
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
            newFraction._numerator *= right._numerator;
            
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

            newFraction._numerator *= right._denominator;
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
            newFraction._denominator *= right;

            newFraction.CheckSign();
            newFraction.CheckForReduction();

            return newFraction;
        }
        public static Fraction operator /(Fraction left, int right)
        {
            return DivisionByInt(left, right);
        }

        public Fraction GetSquareRootNewTon(Fraction eps)
        {
            if(eps <= new Fraction(0,1))
            {
                throw new Exception("Invalid pes, <=0");
            }

            if(this.Denominator < 0)
            {
                throw new Exception("Invalid argument, <0");
            }

            var approxim = new Fraction(1, 1);
            var abs = approxim * approxim - this;
            abs._denominator = BigInteger.Abs(abs._denominator);
            while (abs > eps)
            {
                
                var div = this / approxim;
                //Console.WriteLine($"{approxim} {div}");
                approxim = (div + approxim) / 2;

                abs = approxim * approxim - this;
                abs._denominator = BigInteger.Abs(abs._denominator);
            }
            return approxim;
        }
        #endregion

        public Fraction Abs()
        {
            if(Denominator < 0)
            {
                return new Fraction(Numerator, -Denominator);
            } else
            {
                return new Fraction(Numerator, Denominator);
            }
        }

        public double ToDouble()
        {
            double omg_n = (double)this._numerator;
            double omg_d = (double)this._denominator;
            return omg_n / omg_d; 
        }
        public string ToDecimalFractionString(BigInteger figuresAfterDotQuantity)
        {
            var full = BigInteger.DivRem(this._numerator, this._denominator, out var rem);
            

            var sb = new StringBuilder();
            if (this.Denominator < 0)
            {
                sb.Append("-");
            }
            sb.Append(full.ToString());
            sb.Append('.');
            
            for (int i = 0; i < figuresAfterDotQuantity; i++)
            {
                rem *= 10;
                sb.Append(BigInteger.Abs(BigInteger.DivRem(rem, this._denominator, out rem)));
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var temp = "-";
            if (this._denominator > 0)
            {
                temp = "";
            }
            if(this._denominator == 1)
            {
                return $"{temp}{this._numerator}";
            }
            return $"{temp}{this._numerator}/{BigInteger.Abs(this._denominator)}";
        }

        #region Equals
        public bool Equals(Fraction other)
        {
            if (other == null)
                return false;

            if (this._numerator == other._numerator && this._denominator == other._denominator)
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
            if (ReferenceEquals((fraction1), null) || ReferenceEquals((fraction2), null))
                return Object.Equals(fraction1, fraction2);

            return fraction1.Equals(fraction2);
        }

        public static bool operator !=(Fraction fraction1, Fraction fraction2)
        {
            if ((fraction1) == null || (fraction2) == null)
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

            if (this._denominator > 0 && obj._denominator < 0) return 1;
            if (this._denominator < 0 && obj._denominator > 0) return -1;

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
