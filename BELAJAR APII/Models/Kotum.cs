using System;
using System.Collections.Generic;

namespace BELAJAR_APII.Models;

public partial class Kotum
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public virtual ICollection<Sekolah> Sekolahs { get; set; } = new List<Sekolah>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
