using System;
using System.Collections.Generic;

namespace BELAJAR_APII.Models;

public partial class Sekolah
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public int KotaId { get; set; }

    public virtual Kotum? Kota { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
