	
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace NASDMS.RDS
{ 
    // ############################################################################
    // #
    // #        ---==>  T H I S  F I L E  W A S  G E N E R A T E D  <==---
    // #
    // # This file was generated by the CodeGen Prototype
    // # Generated on 3/23/2017 12:04:08 PM
    // #
    // # Edits to this file may cause incorrect behavior and will be lost
    // # if the code is regenerated.
    // #
    // ############################################################################
	
	// NASDMS Domain objects
	
	public partial class AuditTrail : Entity<AuditTrail>
	{
		public AuditTrail() { }
		public AuditTrail(bool defaults) : base(defaults) { }

        public int? Id { get; set; }
        public string Oid { get; set; }
        public string Data { get; set; }
        public string ChangedBy { get; set; }
        public int Category { get; set; }
        public int Action { get; set; }
        public string Myself { get; set; }

        public DateTime? ChangedOn { get; set; }
    }
}
