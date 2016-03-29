//
// Author: Phil Crosby
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.

using System;
using System.Text;
using System.Collections.Generic;

namespace InstallPad
{
    public abstract class Persistable
    {
        private List<string> m_xmlErrors = new List<string>();
        private List<string> m_appErrors = new List<string>();

        public List<string> XmlErrors
        {
            get { return m_xmlErrors; }
        }

        public List<string> AppErrors
        {
            get { return m_appErrors; }
        }

        public abstract bool Validate();
    }
}
