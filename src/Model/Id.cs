namespace BlackDigital.Model
{
    /// <summary>
    /// Represents a flexible identifier that can be constructed from and converted to strings, GUIDs, and various numeric types.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a string value.
        /// </summary>
        /// <param name="value">The identifier string, or <c>null</c>.</param>
        public Id(string? value)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="Guid"/>, encoding it as a URL-safe base64 string.
        /// </summary>
        /// <param name="guid">The GUID to encode, or <c>null</c>.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="short"/> value.
        /// </summary>
        /// <param name="value">The short value, or <c>null</c>.</param>
        public Id(short? value)
        {
            _value = value?.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="ushort"/> value.
        /// </summary>
        /// <param name="value">The unsigned short value, or <c>null</c>.</param>
        public Id(ushort? value)
        {
            _value = value?.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from an <see cref="int"/> value.
        /// </summary>
        /// <param name="value">The integer value, or <c>null</c>.</param>
        public Id(int? value)
        {
            _value = value?.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="uint"/> value.
        /// </summary>
        /// <param name="value">The unsigned integer value, or <c>null</c>.</param>
        public Id(uint? value)
        {
            _value = value?.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="long"/> value.
        /// </summary>
        /// <param name="value">The long value, or <c>null</c>.</param>
        public Id(long? value)
        {
            _value = value?.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id"/> struct from a <see cref="ulong"/> value.
        /// </summary>
        /// <param name="value">The unsigned long value, or <c>null</c>.</param>
        public Id(ulong? value)
        {
            _value = value?.ToString();
        }

        #endregion "Construction"

        #region "Properties"

        private readonly string? _value;

        /// <summary>
        /// Gets a value indicating whether the identifier has a non-empty value.
        /// </summary>
        public bool HasId => !string.IsNullOrWhiteSpace(_value);

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Creates a new unique <see cref="Id"/> from a new <see cref="Guid"/>.
        /// </summary>
        /// <returns>A new unique identifier.</returns>
        public static Id CreateId() => Guid.NewGuid();

        /// <summary>
        /// Returns the hash code for this identifier.
        /// </summary>
        /// <returns>A hash code for the current identifier.</returns>
        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        #endregion "Methods"

        #region "CompareTo"

        /// <summary>
        /// Compares this identifier with another <see cref="Id"/>.
        /// </summary>
        /// <param name="other">The other identifier to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(Id other)
        {
            return string.Compare(this, other._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a string.
        /// </summary>
        /// <param name="other">The string to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(string? other)
        {
            return string.Compare(this, other, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="Guid"/>.
        /// </summary>
        /// <param name="other">The GUID to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(Guid other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="short"/>.
        /// </summary>
        /// <param name="other">The short value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(short other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="ushort"/>.
        /// </summary>
        /// <param name="other">The unsigned short value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(ushort other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with an <see cref="int"/>.
        /// </summary>
        /// <param name="other">The integer value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(int other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="uint"/>.
        /// </summary>
        /// <param name="other">The unsigned integer value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(uint other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="long"/>.
        /// </summary>
        /// <param name="other">The long value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(long other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Compares this identifier with a <see cref="ulong"/>.
        /// </summary>
        /// <param name="other">The unsigned long value to compare.</param>
        /// <returns>A value indicating the relative order of the identifiers.</returns>
        public int CompareTo(ulong other)
        {
            return string.Compare(this, ((Id)other)._value, StringComparison.CurrentCulture);
        }

        #endregion "CompareTo"

        #region "Equals"

        /// <summary>
        /// Determines whether the specified object is equal to the current identifier.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the object is equal to the current identifier; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            return obj switch
            {
                Id otherId => Equals(otherId),
                string otherString => Equals(otherString),
                Guid otherGuid => Equals(otherGuid),
                short otherShort => Equals(otherShort),
                ushort otherUShort => Equals(otherUShort),
                int otherInt => Equals(otherInt),
                uint otherUInt => Equals(otherUInt),
                long otherLong => Equals(otherLong),
                ulong otherULong => Equals(otherULong),
                _ => false,
            };
        }

        /// <summary>
        /// Determines whether the specified <see cref="Id"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The identifier to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Id other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Determines whether the specified string is equal to the current identifier.
        /// </summary>
        /// <param name="other">The string to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(string? other)
        {
            return _value == other;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Guid"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The GUID to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Guid other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="short"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The short value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(short other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ushort"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The unsigned short value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ushort other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="int"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The integer value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(int other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="uint"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The unsigned integer value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(uint other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="long"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The long value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(long other)
        {
            return _value == ((Id)other)._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ulong"/> is equal to the current identifier.
        /// </summary>
        /// <param name="other">The unsigned long value to compare.</param>
        /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ulong other)
        {
            return _value == ((Id)other)._value;
        }

        #endregion "Equals"

        #region "To..."

        /// <summary>
        /// Returns the string representation of the identifier.
        /// </summary>
        /// <returns>The identifier string, or an empty string if no value is set.</returns>
        override public string ToString() => _value ?? string.Empty;

        /// <summary>
        /// Converts the identifier to a <see cref="Guid"/>.
        /// </summary>
        /// <returns>The decoded GUID, or <see cref="Guid.Empty"/> if the identifier is empty.</returns>
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

        /// <summary>
        /// Tries to convert the identifier to a <see cref="Guid"/>.
        /// </summary>
        /// <returns>The decoded GUID, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to a <see cref="short"/>.
        /// </summary>
        /// <returns>The parsed short value, or 0 if the identifier is empty.</returns>
        public short ToShort()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return short.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to a <see cref="short"/>.
        /// </summary>
        /// <returns>The parsed short value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to a <see cref="ushort"/>.
        /// </summary>
        /// <returns>The parsed unsigned short value, or 0 if the identifier is empty.</returns>
        public ushort ToUShort()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return ushort.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to a <see cref="ushort"/>.
        /// </summary>
        /// <returns>The parsed unsigned short value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to an <see cref="int"/>.
        /// </summary>
        /// <returns>The parsed integer value, or 0 if the identifier is empty.</returns>
        public int ToInt()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return int.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to an <see cref="int"/>.
        /// </summary>
        /// <returns>The parsed integer value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to a <see cref="uint"/>.
        /// </summary>
        /// <returns>The parsed unsigned integer value, or 0 if the identifier is empty.</returns>
        public uint ToUInt()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return uint.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to a <see cref="uint"/>.
        /// </summary>
        /// <returns>The parsed unsigned integer value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to a <see cref="long"/>.
        /// </summary>
        /// <returns>The parsed long value, or 0 if the identifier is empty.</returns>
        public long ToLong()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return long.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to a <see cref="long"/>.
        /// </summary>
        /// <returns>The parsed long value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Converts the identifier to a <see cref="ulong"/>.
        /// </summary>
        /// <returns>The parsed unsigned long value, or 0 if the identifier is empty.</returns>
        public ulong ToULong()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return 0;

            return ulong.Parse(_value);
        }

        /// <summary>
        /// Tries to convert the identifier to a <see cref="ulong"/>.
        /// </summary>
        /// <returns>The parsed unsigned long value, or <c>null</c> if conversion fails.</returns>
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

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="Guid"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid GUID; otherwise, <c>false</c>.</returns>
        public bool IsGuid()
        {
            return TryToGuid() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="short"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid short; otherwise, <c>false</c>.</returns>
        public bool IsShort()
        {
            return TryToShort() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="ushort"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid unsigned short; otherwise, <c>false</c>.</returns>
        public bool IsUShort()
        {
            return TryToUShort() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to an <see cref="int"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid integer; otherwise, <c>false</c>.</returns>
        public bool IsInt()
        {
            return TryToInt() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="uint"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid unsigned integer; otherwise, <c>false</c>.</returns>
        public bool IsUInt()
        {
            return TryToUInt() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="long"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid long; otherwise, <c>false</c>.</returns>
        public bool IsLong()
        {
            return TryToLong() != null;
        }

        /// <summary>
        /// Determines whether the identifier can be converted to a <see cref="ulong"/>.
        /// </summary>
        /// <returns><c>true</c> if the identifier is a valid unsigned long; otherwise, <c>false</c>.</returns>
        public bool IsULong()
        {
            return TryToULong() != null;
        }

        #region "Operator To Id"

        /// <summary>
        /// Converts a string to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(string id) => new(id);

        /// <summary>
        /// Converts a <see cref="Guid"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(Guid id) => new(id);

        /// <summary>
        /// Converts a <see cref="short"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(short id) => new(id);

        /// <summary>
        /// Converts a <see cref="ushort"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(ushort id) => new(id);

        /// <summary>
        /// Converts an <see cref="int"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(int id) => new(id);

        /// <summary>
        /// Converts a <see cref="uint"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(uint id) => new(id);

        /// <summary>
        /// Converts a <see cref="long"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(long id) => new(id);

        /// <summary>
        /// Converts a <see cref="ulong"/> to an <see cref="Id"/>.
        /// </summary>
        public static implicit operator Id(ulong id) => new(id);

        #endregion "Operator To Id"

        #region "Operator From Id"

        /// <summary>
        /// Converts an <see cref="Id"/> to a string.
        /// </summary>
        public static implicit operator string(Id id) => id._value ?? string.Empty;

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="Guid"/>.
        /// </summary>
        public static implicit operator Guid(Id id) => id.ToGuid();

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="short"/>.
        /// </summary>
        public static implicit operator short(Id id) => id.ToShort();

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="ushort"/>.
        /// </summary>
        public static implicit operator ushort(Id id) => id.ToUShort();

        /// <summary>
        /// Converts an <see cref="Id"/> to an <see cref="int"/>.
        /// </summary>
        public static implicit operator int(Id id) => id.ToInt();

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="uint"/>.
        /// </summary>
        public static implicit operator uint(Id id) => id.ToUInt();

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="long"/>.
        /// </summary>
        public static implicit operator long(Id id) => id.ToLong();

        /// <summary>
        /// Converts an <see cref="Id"/> to a <see cref="ulong"/>.
        /// </summary>
        public static implicit operator ulong(Id id) => id.ToULong();

        #endregion "Operator From Id"

        #region "Operator Compare"

        /// <summary>
        /// Determines whether two identifiers are equal.
        /// </summary>
        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two identifiers are not equal.
        /// </summary>
        public static bool operator !=(Id left, Id right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the left identifier is less than the right identifier.
        /// </summary>
        public static bool operator <(Id left, Id right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the left identifier is less than or equal to the right identifier.
        /// </summary>
        public static bool operator <=(Id left, Id right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether the left identifier is greater than the right identifier.
        /// </summary>
        public static bool operator >(Id left, Id right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the left identifier is greater than or equal to the right identifier.
        /// </summary>
        public static bool operator >=(Id left, Id right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion "Operator Compare"
    }
}
