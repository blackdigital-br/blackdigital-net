namespace BlackDigital.Model
{
    public readonly struct Id : IComparable<Id>,
                                IComparable<string>,
                                IComparable<Guid>,
                                IComparable<short>,
                                IComparable<ushort>,
                                IComparable<int>,
                                IComparable<uint>,
                                IComparable<long>,
                                IComparable<ulong>,
                                IEquatable<string>,
                                IEquatable<Id>,
                                IEquatable<Guid>,
                                IEquatable<short>,
                                IEquatable<ushort>,
                                IEquatable<int>,
                                IEquatable<uint>,
                                IEquatable<long>,
                                IEquatable<ulong>

    {
        #region "Construction"

        public Id(string? value)
        {
            _value = value;
        }

        public Id(Guid? guid)
        {
            if (guid.HasValue)
            {
                _value = Convert.ToBase64String(guid.Value.ToByteArray())
                                                    .Replace("=", "")
                                                    .Replace("+", "_")
                                                    .Replace("/", "-");
            }
            else
            {
                _value = null;
            }
        }

        public Id(short? value)
        {
            _value = value?.ToString();
        }

        public Id(ushort? value)
        {
            _value = value?.ToString();
        }

        public Id(int? value)
        {
            _value = value?.ToString();
        }

        public Id(uint? value)
        {
            _value = value?.ToString();
        }

        public Id(long? value)
        {
            _value = value?.ToString();
        }

        public Id(ulong? value)
        {
            _value = value?.ToString();
        }

        #endregion "Construction"

        #region "Properties"

        private readonly string? _value;

        public bool HasId => !string.IsNullOrWhiteSpace(_value);

        #endregion "Properties"

        #region "Methods"

        public static Id CreateId() => Guid.NewGuid();

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        #endregion "Methods"

        #region "CompareTo"

        public int CompareTo(Id other)
        {
            return string.Compare(this, other._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(string? other)
        {
            return string.Compare(this, other, StringComparison.CurrentCulture);
        }

        public int CompareTo(Guid other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(short other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(ushort other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(int other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(uint other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(long other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        public int CompareTo(ulong other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        #endregion "CompareTo"

        #region "Equals"

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            string typeName = obj.GetType().FullName;

            return typeName switch
            {
                "BlackDigital.Id" => Equals((Id)obj),
                "System.String" => Equals((string)obj),
                "System.Guid" => Equals((Guid)obj),
                "System.Int16" => Equals((short)obj),
                "System.UInt16" => Equals((ushort)obj),
                "System.Int32" => Equals((int)obj),
                "System.UInt32" => Equals((uint)obj),
                "System.Int64" => Equals((long)obj),
                "System.UInt64" => Equals((ulong)obj),
                _ => false,
            };
        }

        public bool Equals(Id other)
        {
            return _value == other._value;
        }

        public bool Equals(string? other)
        {
            return _value == other;
        }

        public bool Equals(Guid other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(short other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(ushort other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(int other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(uint other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(long other)
        {
            return _value == ((Id)other)._value;
        }

        public bool Equals(ulong other)
        {
            return _value == ((Id)other)._value;
        }

        #endregion "Equals"

        #region "To..."

        override public string ToString() => _value ?? string.Empty;

        public Guid ToGuid()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return Guid.Empty;

            var completeGuid = _value.Replace("-", "/")
                                     .Replace("_", "+");

            while (completeGuid.Length % 4 != 0)
                completeGuid += "=";

            return new Guid(Convert.FromBase64String(completeGuid));
        }

        public Guid? TryToGuid()
        {
            try
            {
                return ToGuid();
            }
            catch
            {
                return null;
            }
        }

        public short ToShort()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return short.Parse(_value);
        }

        public short? TryToShort()
        {
            try
            {
                return ToShort();
            }
            catch
            {
                return null;
            }
        }

        public ushort ToUShort()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return ushort.Parse(_value);
        }

        public ushort? TryToUShort()
        {
            try
            {
                return ToUShort();
            }
            catch
            {
                return null;
            }
        }

        public int ToInt()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return int.Parse(_value);
        }

        public int? TryToInt()
        {
            try
            {
                return ToInt();
            }
            catch
            {
                return null;
            }
        }

        public uint ToUInt()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return uint.Parse(_value);
        }

        public uint? TryToUInt()
        {
            try
            {
                return ToUInt();
            }
            catch
            {
                return null;
            }
        }

        public long ToLong()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return long.Parse(_value);
        }

        public long? TryToLong()
        {
            try
            {
                return ToLong();
            }
            catch
            {
                return null;
            }
        }

        public ulong ToULong()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return ulong.Parse(_value);
        }

        public ulong? TryToULong()
        {
            try
            {
                return ToULong();
            }
            catch
            {
                return null;
            }
        }

        #endregion "To..."

        #region "Operator To Id"

        public static implicit operator Id(string id) => new(id);

        public static implicit operator Id(Guid id) => new(id);

        public static implicit operator Id(short id) => new(id);

        public static implicit operator Id(ushort id) => new(id);

        public static implicit operator Id(int id) => new(id);

        public static implicit operator Id(uint id) => new(id);

        public static implicit operator Id(long id) => new(id);

        public static implicit operator Id(ulong id) => new(id);

        #endregion "Operator To Id"

        #region "Operator From Id"

        public static implicit operator string(Id id) => id._value ?? string.Empty;

        public static implicit operator Guid(Id id) => id.ToGuid();

        public static implicit operator short(Id id) => id.ToShort();

        public static implicit operator ushort(Id id) => id.ToUShort();

        public static implicit operator int(Id id) => id.ToInt();

        public static implicit operator uint(Id id) => id.ToUInt();

        public static implicit operator long(Id id) => id.ToLong();

        public static implicit operator ulong(Id id) => id.ToULong();

        #endregion "Operator From Id"

        #region "Operator Compare"

        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id left, Id right)
        {
            return !(left == right);
        }

        public static bool operator <(Id left, Id right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Id left, Id right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Id left, Id right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Id left, Id right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion "Operator Compare"
    }
}
