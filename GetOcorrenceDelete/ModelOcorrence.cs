using System;

namespace GetOcorrenceDelete
{
    public class ModelOcorrence
    {
        public int codDigitalDoc { get; set; }
        public int codDigital { get; set; }
        public string document { get; set; }
        public string codCheckList { get; set; }
        public string codGrupo { get; set; }
        public int configCodUser { get; set; }
        public string dateInclude { get; set; }
        public string aliasName { get; set; }
        public bool migrated { get; set; }
        public bool internalDocument { get; set; }
        public string codDigitalPai { get; set; }
        public int tryCounter { get; set; }
        public string pagesSize { get; set; }
        public string migratedDate { get; set; }
        public string attempt { get; set; }
        public string obs { get; set; }
        public string robot { get; set; }
    }
}