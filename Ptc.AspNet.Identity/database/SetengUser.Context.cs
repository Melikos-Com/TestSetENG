﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ptc.AspNet.Identity.database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SetengUser : DbContext
    {
        public SetengUser()
            : base("name=SetengUser")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TAcceptedLog> TAcceptedLog { get; set; }
        public virtual DbSet<TAJCREP> TAJCREP { get; set; }
        public virtual DbSet<TAJCYCL> TAJCYCL { get; set; }
        public virtual DbSet<TAJKIND> TAJKIND { get; set; }
        public virtual DbSet<TAPPVER> TAPPVER { get; set; }
        public virtual DbSet<TAREANM> TAREANM { get; set; }
        public virtual DbSet<TASSETS> TASSETS { get; set; }
        public virtual DbSet<TASTIMG> TASTIMG { get; set; }
        public virtual DbSet<TASTKND> TASTKND { get; set; }
        public virtual DbSet<TAUTONO> TAUTONO { get; set; }
        public virtual DbSet<TBFFDTL> TBFFDTL { get; set; }
        public virtual DbSet<TBFFMST> TBFFMST { get; set; }
        public virtual DbSet<TBFSPCM> TBFSPCM { get; set; }
        public virtual DbSet<TBFYOUD> TBFYOUD { get; set; }
        public virtual DbSet<TBFYOUM> TBFYOUM { get; set; }
        public virtual DbSet<TBULIMG> TBULIMG { get; set; }
        public virtual DbSet<TBULTIN> TBULTIN { get; set; }
        public virtual DbSet<TBUPART> TBUPART { get; set; }
        public virtual DbSet<TCALERR> TCALERR { get; set; }
        public virtual DbSet<TCALEXT> TCALEXT { get; set; }
        public virtual DbSet<TCALIMG> TCALIMG { get; set; }
        public virtual DbSet<TCALINV> TCALINV { get; set; }
        public virtual DbSet<TCALLER> TCALLER { get; set; }
        public virtual DbSet<TCallLogRecord> TCallLogRecord { get; set; }
        public virtual DbSet<TCALLOG> TCALLOG { get; set; }
        public virtual DbSet<TCallogCourse> TCallogCourse { get; set; }
        public virtual DbSet<TCALSTA> TCALSTA { get; set; }
        public virtual DbSet<TCFMTN> TCFMTN { get; set; }
        public virtual DbSet<TCFMTNL> TCFMTNL { get; set; }
        public virtual DbSet<TCFRANK> TCFRANK { get; set; }
        public virtual DbSet<TCMPDAT> TCMPDAT { get; set; }
        public virtual DbSet<TCTLINV> TCTLINV { get; set; }
        public virtual DbSet<TDAMAGE> TDAMAGE { get; set; }
        public virtual DbSet<TDAMMTN> TDAMMTN { get; set; }
        public virtual DbSet<TDAYCTL> TDAYCTL { get; set; }
        public virtual DbSet<TDZCREP> TDZCREP { get; set; }
        public virtual DbSet<TDZKIND> TDZKIND { get; set; }
        public virtual DbSet<TDZPQTY> TDZPQTY { get; set; }
        public virtual DbSet<TDZUPRC> TDZUPRC { get; set; }
        public virtual DbSet<TELECDE> TELECDE { get; set; }
        public virtual DbSet<TELEPAR> TELEPAR { get; set; }
        public virtual DbSet<TENGMST> TENGMST { get; set; }
        public virtual DbSet<TENGSTK> TENGSTK { get; set; }
        public virtual DbSet<TEXPFIL> TEXPFIL { get; set; }
        public virtual DbSet<TFINISH> TFINISH { get; set; }
        public virtual DbSet<TINVDTL> TINVDTL { get; set; }
        public virtual DbSet<TINVMST> TINVMST { get; set; }
        public virtual DbSet<TKNDHST> TKNDHST { get; set; }
        public virtual DbSet<TLAYHST> TLAYHST { get; set; }
        public virtual DbSet<TLAYKND> TLAYKND { get; set; }
        public virtual DbSet<TLAYOUTIMG> TLAYOUTIMG { get; set; }
        public virtual DbSet<TLVCREP> TLVCREP { get; set; }
        public virtual DbSet<TLVKIND> TLVKIND { get; set; }
        public virtual DbSet<TLVRHST> TLVRHST { get; set; }
        public virtual DbSet<TLYIMGM> TLYIMGM { get; set; }
        public virtual DbSet<TPAPERS> TPAPERS { get; set; }
        public virtual DbSet<TPGMAUT> TPGMAUT { get; set; }
        public virtual DbSet<TPGMEXE> TPGMEXE { get; set; }
        public virtual DbSet<TPGMMST> TPGMMST { get; set; }
        public virtual DbSet<TPGMUSR> TPGMUSR { get; set; }
        public virtual DbSet<TPRJACT> TPRJACT { get; set; }
        public virtual DbSet<TPRJDTL> TPRJDTL { get; set; }
        public virtual DbSet<TPRJMST> TPRJMST { get; set; }
        public virtual DbSet<TPRVMST> TPRVMST { get; set; }
        public virtual DbSet<TRECLOG> TRECLOG { get; set; }
        public virtual DbSet<TREFDAT> TREFDAT { get; set; }
        public virtual DbSet<TREFDAT_Test> TREFDAT_Test { get; set; }
        public virtual DbSet<TRSTDTA> TRSTDTA { get; set; }
        public virtual DbSet<TRSTNAM> TRSTNAM { get; set; }
        public virtual DbSet<TSBFYOU> TSBFYOU { get; set; }
        public virtual DbSet<TSENDMAILVND> TSENDMAILVND { get; set; }
        public virtual DbSet<TSTAIPD> TSTAIPD { get; set; }
        public virtual DbSet<TSTAIPM> TSTAIPM { get; set; }
        public virtual DbSet<TSTASTM> TSTASTM { get; set; }
        public virtual DbSet<TSTRAST> TSTRAST { get; set; }
        public virtual DbSet<TSTRHST> TSTRHST { get; set; }
        public virtual DbSet<TSTRKND> TSTRKND { get; set; }
        public virtual DbSet<TSTRLAY> TSTRLAY { get; set; }
        public virtual DbSet<TSTRMEM> TSTRMEM { get; set; }
        public virtual DbSet<TSTRMOD> TSTRMOD { get; set; }
        public virtual DbSet<TSTRMST> TSTRMST { get; set; }
        public virtual DbSet<TSTRMZO> TSTRMZO { get; set; }
        public virtual DbSet<TSTRPWR> TSTRPWR { get; set; }
        public virtual DbSet<TSTRPWR_Wait_LOG> TSTRPWR_Wait_LOG { get; set; }
        public virtual DbSet<TSTRTMP> TSTRTMP { get; set; }
        public virtual DbSet<TSTSAMD> TSTSAMD { get; set; }
        public virtual DbSet<TSTSAMM> TSTSAMM { get; set; }
        public virtual DbSet<TSTVNDM> TSTVNDM { get; set; }
        public virtual DbSet<TSYSDAT> TSYSDAT { get; set; }
        public virtual DbSet<TSYSLOG> TSYSLOG { get; set; }
        public virtual DbSet<TSYSROL> TSYSROL { get; set; }
        public virtual DbSet<TTechnicianGroup> TTechnicianGroup { get; set; }
        public virtual DbSet<TTechnicianGroupClaims> TTechnicianGroupClaims { get; set; }
        public virtual DbSet<TTNSCALH> TTNSCALH { get; set; }
        public virtual DbSet<TTNSCALT> TTNSCALT { get; set; }
        public virtual DbSet<TTNSCFFH> TTNSCFFH { get; set; }
        public virtual DbSet<TTNSCFFT> TTNSCFFT { get; set; }
        public virtual DbSet<TTNSCFMH> TTNSCFMH { get; set; }
        public virtual DbSet<TTNSCFMT> TTNSCFMT { get; set; }
        public virtual DbSet<TTNSCLFH> TTNSCLFH { get; set; }
        public virtual DbSet<TTNSCLFT> TTNSCLFT { get; set; }
        public virtual DbSet<TUpFile> TUpFile { get; set; }
        public virtual DbSet<TUSERHC> TUSERHC { get; set; }
        public virtual DbSet<TUSRDTL> TUSRDTL { get; set; }
        public virtual DbSet<TUSREXT> TUSREXT { get; set; }
        public virtual DbSet<TUSRMST> TUSRMST { get; set; }
        public virtual DbSet<TUSRVENRELATION> TUSRVENRELATION { get; set; }
        public virtual DbSet<TVBULTIN> TVBULTIN { get; set; }
        public virtual DbSet<TVBULTIN_ReadRecord> TVBULTIN_ReadRecord { get; set; }
        public virtual DbSet<TVENDER> TVENDER { get; set; }
        public virtual DbSet<TVenderTechnician> TVenderTechnician { get; set; }
        public virtual DbSet<TVNDAST> TVNDAST { get; set; }
        public virtual DbSet<TVNDCNT> TVNDCNT { get; set; }
        public virtual DbSet<TVNDCON> TVNDCON { get; set; }
        public virtual DbSet<TVNDENG> TVNDENG { get; set; }
        public virtual DbSet<TVNDMEM> TVNDMEM { get; set; }
        public virtual DbSet<TVNDPOP> TVNDPOP { get; set; }
        public virtual DbSet<TVNDSHOW> TVNDSHOW { get; set; }
        public virtual DbSet<TVNDTMP> TVNDTMP { get; set; }
        public virtual DbSet<TVNDUSR> TVNDUSR { get; set; }
        public virtual DbSet<TVNDUSRD> TVNDUSRD { get; set; }
        public virtual DbSet<TVNDZO> TVNDZO { get; set; }
        public virtual DbSet<TVNOTICE> TVNOTICE { get; set; }
        public virtual DbSet<TVNTCRD> TVNTCRD { get; set; }
        public virtual DbSet<TVOUCH> TVOUCH { get; set; }
        public virtual DbSet<TVOUCTL> TVOUCTL { get; set; }
        public virtual DbSet<TVPRICE> TVPRICE { get; set; }
        public virtual DbSet<TVZOAST> TVZOAST { get; set; }
        public virtual DbSet<TWRKKND> TWRKKND { get; set; }
        public virtual DbSet<TWSHCYC> TWSHCYC { get; set; }
        public virtual DbSet<TZOCODE> TZOCODE { get; set; }
        public virtual DbSet<Migration_Default_TCMPDAT> Migration_Default_TCMPDAT { get; set; }
        public virtual DbSet<TAJCREP_LOG> TAJCREP_LOG { get; set; }
        public virtual DbSet<TAJCYCL_LOG> TAJCYCL_LOG { get; set; }
        public virtual DbSet<TAPPLOG> TAPPLOG { get; set; }
        public virtual DbSet<TASSETS_LOG> TASSETS_LOG { get; set; }
        public virtual DbSet<TBULTIN_LOG> TBULTIN_LOG { get; set; }
        public virtual DbSet<TCALINV_LOG> TCALINV_LOG { get; set; }
        public virtual DbSet<TCALLOG_LOG> TCALLOG_LOG { get; set; }
        public virtual DbSet<TCMPDAT_LOG> TCMPDAT_LOG { get; set; }
        public virtual DbSet<TDAMMTN_LOG> TDAMMTN_LOG { get; set; }
        public virtual DbSet<TDZCREP_LOG> TDZCREP_LOG { get; set; }
        public virtual DbSet<TENGMST_LOG> TENGMST_LOG { get; set; }
        public virtual DbSet<TFINISH_LOG> TFINISH_LOG { get; set; }
        public virtual DbSet<TPGMAUT_20171013BackUp> TPGMAUT_20171013BackUp { get; set; }
        public virtual DbSet<TPGMAUT_20171025BackUp> TPGMAUT_20171025BackUp { get; set; }
        public virtual DbSet<TSENDMAIL> TSENDMAIL { get; set; }
        public virtual DbSet<TSTRKND_LOG> TSTRKND_LOG { get; set; }
        public virtual DbSet<TSTRMOD_LOG> TSTRMOD_LOG { get; set; }
        public virtual DbSet<TSYSDAT_LOG> TSYSDAT_LOG { get; set; }
        public virtual DbSet<TTNSCALT__Test> TTNSCALT__Test { get; set; }
        public virtual DbSet<TVPRICE_LOG> TVPRICE_LOG { get; set; }
        public virtual DbSet<TVUPART> TVUPART { get; set; }
        public virtual DbSet<TWRKKND_LOG> TWRKKND_LOG { get; set; }
    }
}
