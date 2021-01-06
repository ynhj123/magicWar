using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page<T>
{
    List<T> list;
    int count;

    public List<T> List { get => list; set => list = value; }
    public int Count { get => count; set => count = value; }

    private Page() { }

    public static Page<T> build() { return new Page<T>(); }

    public Page<T> SetList(List<T> list)
    {
        this.list = list;
        return this;
    }
    public Page<T> SetCount(int count)
    {
        this.count = count;
        return this;
    }
}
