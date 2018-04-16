using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigInteger
{
    class BigInteger
    {
        //field
        public Byte[] Number { get; private set; }
        private  Byte _len;
        public Byte Length
        {
            get { return _len; }
        }
        private Sign _sign;

        //constructors
        public BigInteger()
        {
            _len = 1;
            _sign = Sign.Positive;
            Number = new Byte[_len];
            Number[0] = 0;
        }
        private BigInteger(Byte len)
        {
            _len = len;
            _sign = Sign.Positive;
            Number = new Byte[_len];
        }


        //operations
        public static BigInteger Add(BigInteger left, BigInteger right)
        {
            if (left._sign == Sign.Negative && right._sign == Sign.Positive)
            {
                left._sign = Sign.Positive;
                return Subtract(right, left);
            }
            if (right._sign == Sign.Negative && left._sign == Sign.Positive)
            {
                right._sign = Sign.Positive;
                return Subtract(left, right);
            }


            BigInteger res;
            Byte maxLen = Math.Max(left.Length, right.Length);
            Byte minLen = Math.Min(left.Length, right.Length);
            BigInteger maxNum, minNum;
            res = new BigInteger(maxLen);
            if (right._sign == Sign.Negative &&left._sign == Sign.Negative)
            {
                res._sign = Sign.Negative;
            }

            if (left < right)
            {
                maxNum = right;
                minNum = left;
            }
            else if (left > right)
            {
                maxNum = left;
                minNum = right;
            }
            else
            {
                maxNum = left;
                minNum = right;
            }

            Byte interval = (Byte)(maxLen - minLen);// for numbers 234 and 3 interval = 2
            Byte memorizer = 0;

            for (int i = maxLen - 1; i >= 0; i--)
            {
                if (i >= interval)
                {
                    Byte sum = (Byte)(maxNum[i] + minNum[i - interval]);
                    res[i] = (Byte)(sum % 10 + memorizer);
                    memorizer = (Byte)(sum / 10);
                }
                else
                {
                    res[i] = maxNum[i];
                }
            }
            if (memorizer > 0)
            {
                BigInteger temp = res;
                res = new BigInteger((Byte)(maxLen + 1));
                res[0] = memorizer;
                for (int i = 1; i < res.Length; i++)
                {
                    res[i] = temp[i - 1];
                }
            }
            return res;
        }
        public static BigInteger Subtract(BigInteger left, BigInteger right)
        {
            if (left._sign == Sign.Negative && right._sign == Sign.Positive)
            {
                right._sign = Sign.Negative;
                return Add(left, right);
            }
            if (left._sign == Sign.Positive && right._sign == Sign.Negative)
            {
                right._sign = Sign.Positive;
                return Add(left, right);
            }
            BigInteger res;
            Byte maxLen = Math.Max(left.Length, right.Length);
            Byte minLen = Math.Min(left.Length, right.Length);
            BigInteger maxNum, minNum;
            res = new BigInteger(maxLen);
            if (left < right)
            {
                res._sign = Sign.Negative;
                maxNum = right;
                minNum = left;
            }
            else if (left > right)
            {
                maxNum = left;
                minNum = right;
            }
            else
            {
                return 0;
            }

            Byte interval = (Byte)(maxLen - minLen);// for numbers 234 and 5 interval = 2
            Byte memorizer = 0;

            for (int i = maxLen - 1; i >= 0; i--)
            {
                bool lesszero = false;
                if (i >= interval)
                {
                    Byte sum;
                    if (maxNum[i] >= minNum[i - interval])
                    {
                        sum = (Byte)(maxNum[i] - minNum[i - interval]);
                    }
                    else
                    {
                        sum = (Byte)(maxNum[i] - minNum[i - interval] + 10);
                        lesszero = true;
                    }
                    if (memorizer > sum % 10)
                    {
                        sum = (Byte)(sum % 10 - memorizer + 10);
                    }
                    else
                    {
                        sum = (Byte)(sum % 10 - memorizer);
                    }
                    if (lesszero)
                    {
                        memorizer = 1;
                    }
                    else
                    {
                        memorizer = 0;
                    }
                    res[i] = (Byte)(sum % 10);
                }
                else if (memorizer == 0)
                {
                    res[i] = maxNum[i];
                }
            }
            return res;
        }
        public static BigInteger Multiply(BigInteger bi, Int32 val)
        {
            BigInteger res = new BigInteger();
            if (SignInt(val) == Sign.Positive ^ bi._sign == Sign.Positive)
            {
                res._sign = Sign.Negative;
                for (int i = 0; i < Math.Abs(val); i++)
                {
                    res = res + bi;
                }
                res._sign = Sign.Negative;

            }
            else
            {
                res._sign = Sign.Negative;
                for (int i = 0; i < Math.Abs(val); i++)
                {
                    res = res + bi;
                }
                res._sign = Sign.Positive;
            }
            return res;
        }

        private Byte this[int key]
        {
            get => Number[key];
            set => Number[key] = value;
        }

        //operators
        public static implicit operator BigInteger(Int32 number)
        {
            BigInteger bi = new BigInteger(LenInt(number));
            bi._sign = SignInt(number);
            for (int i = bi.Length - 1; i >= 0; i--)
            {
                bi[i] = (Byte)(Math.Abs(number % 10));
                number /= 10;
            }
            return bi;
        }
        public static explicit operator Int32(BigInteger bi)
        {
            Int32 number = 0;
            int j = 1;
            for (int i = bi.Length - 1; i >= 0; i--)
            {
                number += bi[i] * j;
                j *= 10;                
            }
            if (bi._sign == Sign.Negative)
            {
                number *= -1;
            }
            return number;
        }

        public static BigInteger operator +(BigInteger left, BigInteger right) => Add(left, right);
        public static BigInteger operator -(BigInteger left, BigInteger right) => Subtract(left, right);
        public static BigInteger operator *(BigInteger bi, Int32 val) => Multiply(bi, val);

        public static Boolean operator >(BigInteger left, BigInteger right)
        {
            if (left.Length > right.Length) return true;
            else if (right.Length > left.Length) return false;
            else
            {
                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] > right[i]) return true; 
                    else if (left[i] < right[i]) return false;
                    else continue;
                }
                return false;
            }
        }
        public static Boolean operator <(BigInteger left, BigInteger right)
        {
            if (left.Length > right.Length) return false;
            else if (right.Length > left.Length) return true;
            else
            {
                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] > right[i]) return false; 
                    else if (left[i] < right[i]) return true;
                    else continue;
                }
                return false;
            }
        }

        //object overriding
        public override string ToString()
        {
            string result = "";
            switch (_sign)
            {
                case Sign.Positive:
                    break;
                case Sign.Negative:
                    result += "-";
                    break;
            }
            Boolean IsBeginningNumber = false;
            foreach (Byte digit in Number)
            {
                if (digit == 0 && !IsBeginningNumber)
                {
                    continue;
                }
                if (digit != 0)
                {
                    IsBeginningNumber = true;
                }
                result += digit;
            }
            if (result == "")
            {
                result += 0;
            }
            return result;
        }


        //auxiliary methods
        private static Byte LenInt(Int32 number)
        {
            Byte len = 0;
            while (number != 0)
            {
                len++;
                number /= 10;
            }
            return len;
        }
        private static Sign SignInt(Int32 number)
        {
            return (number >= 0) ? Sign.Positive : Sign.Negative;
        }
    }
}
