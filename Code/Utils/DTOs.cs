using System;
using System.Collections.Generic;
using System.Text;

namespace AlexaCommandReader {
    [Serializable]
    public class AlexaMessageDTO {
        public string Skill { get; set; }
        public string Intent { get; set; }

        public AlexaMessageDTO(string skillValue, string intentValue) {
            Skill = skillValue;
            Intent = intentValue;
        }
    }
}
