using System;
using System.Collections.Generic;

namespace BELAJAR_APII.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public int? KotaId { get; set; }

    public int? SekolahId { get; set; }
    //public Sekolah sekolah { get; set; }
    //public Kotum kota { get; set; }
    public virtual Kotum? Kota { get; set; }

    public virtual Sekolah? Sekolah { get; set; }
}
