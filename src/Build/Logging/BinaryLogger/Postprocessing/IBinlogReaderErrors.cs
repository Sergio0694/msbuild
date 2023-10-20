﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Build.Logging
{
    /// <summary>
    /// Type of the error that occurred during reading.
    /// </summary>
    public enum ReaderErrorType
    {
        /// <summary>
        /// The file format of the binlog is not supported by the current reader.
        /// Despite the logs should be supported by older readers - there might be certain format updates that prevent
        ///  such forward compatibility. The binlog file contains the info about the minimum required reader version
        ///  to detect this case.
        /// </summary>
        UnsupportedFileFormat,

        /// <summary>
        /// The encountered event is completely unknown to the reader. It cannot interpret neither a part of it.
        /// </summary>
        UnkownEventType,

        /// <summary>
        /// The encountered event is known to the reader and reader is able to read the event as it knows it.
        /// However there are some extra data (append only extension to the event in future version), that reader cannot interpret,
        ///  it can only skip it.
        /// </summary>
        UnknownEventData,

        /// <summary>
        /// The encountered event is known to the reader, however the reader cannot interpret the data of the event.
        /// This is probably caused by the fact that the event definition changed in the future revision in other than append-only manner.
        /// For this reason reader can only skip the event in full.
        /// </summary>
        UnknownFormatOfEventData,
    }

    public interface IBinlogReaderErrors
    {
        /// <summary>
        /// Receives recoverable errors during reading.
        /// Communicates type of the error, kind of the record that encountered the error and the message detailing the error.
        /// </summary>
        event Action<ReaderErrorType, BinaryLogRecordKind, string>? OnRecoverableReadError;
    }
}
