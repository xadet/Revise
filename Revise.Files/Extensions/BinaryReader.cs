﻿#region License

/**
 * Copyright (C) 2012 Jack Wakefield
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// A collection of extensions for the <see cref="BinaryReader"/> class.
/// </summary>
public static class BinaryReaderExtensions {
    /// <summary>
    /// The default encoding to be used when reading strings.
    /// </summary>
    public static Encoding DefaultEncoding;

    /// <summary>
    /// Initializes the <see cref="BinaryReaderExtensions"/> class.
    /// </summary>
    static BinaryReaderExtensions() {
        DefaultEncoding = Encoding.GetEncoding("EUC-KR");
    }

    /// <summary>
    /// Reads a null-terminated string from the current stream.
    /// </summary>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadNullTerminatedString(this BinaryReader reader) {
        return reader.ReadNullTerminatedString(DefaultEncoding);
    }

    /// <summary>
    /// Reads a null-terminated string from the current stream.
    /// </summary>
    /// <param name="encoding">The character encoding.</param>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadNullTerminatedString(this BinaryReader reader, Encoding encoding) {
        List<byte> values = new List<byte>();
        byte value;

        while((value = reader.ReadByte()) != 0) {
            values.Add(value);
        }

        return encoding.GetString(values.ToArray());
    }

    /// <summary>
    /// Reads a string with a pre-fixed length as a 16-bit integer from the underlying stream.
    /// </summary>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadShortString(this BinaryReader reader) {
        return reader.ReadShortString(DefaultEncoding);
    }

    /// <summary>
    /// Reads a string with a pre-fixed length as a 16-bit integer from the underlying stream.
    /// </summary>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadShortString(this BinaryReader reader, Encoding encoding) {
        return reader.ReadString(reader.ReadInt16(), DefaultEncoding);
    }

    /// <summary>
    /// Reads a fixed-length string from the underlying stream.
    /// </summary>
    /// <param name="length">The length of the string.</param>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadString(this BinaryReader reader, int length) {
        return reader.ReadString(length, DefaultEncoding);
    }

    /// <summary>
    /// Reads a fixed-length string from the underlying stream.
    /// </summary>
    /// <param name="length">The length of the string.</param>
    /// <param name="encoding">The character encoding.</param>
    /// <returns>
    /// The string being read.
    /// </returns>
    public static string ReadString(this BinaryReader reader, int length, Encoding encoding) {
        return encoding.GetString(reader.ReadBytes(length));
    }
}