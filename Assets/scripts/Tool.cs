using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    private static selection selected = Tool.selection.CURSOR;
    public static selection getSelected()
    {
        return selected;
    }

    public static void setSelection(Tool.selection selection)
    {
        Tool.selected = selection;
    }
    public enum selection
    {
        CURSOR, BUILD, REMOVE
    }
}
