using System;
using System.Collections.Generic;

namespace DictationProcessorLib.DataContracts
{
    /* sample metadata: 
    [
        {
            "DateRecorded": "2017-03-05T21:03:42Z",
            "File": {
                "FileName": "01.WAV",
                "Md5Checksum": "3e44b3060c7bffb5aa6dbfc85cb4edd8"
            },
            "Patient": "Angston Lacksley",
            "Practitioner": "Dr. Kingston Loyola",
            "Tags": [
                "checkup"
            ]
        }
    ]
    */
    public class UploadMetadata
    {
        public string Practitioner { get; set; }

        public string Patient { get; set; }

        public DateTime DateRecorded { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public UploadMetadataAudioFile File { get; set; }
    }
}