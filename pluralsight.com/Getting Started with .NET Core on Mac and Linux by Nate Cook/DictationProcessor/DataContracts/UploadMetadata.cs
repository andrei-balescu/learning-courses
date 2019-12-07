using System;
using System.Collections.Generic;

namespace DictationProcessor.DataContracts
{
    public class UploadMetadata
    {
        public string Practitioner { get; set; }

        public string Patient { get; set; }

        public DateTime DateRecorded { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public UploadMetadataAudioFile File { get; set; }
    }
}