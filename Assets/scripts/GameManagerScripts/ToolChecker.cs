using UnityEngine;
using UnityEngine.UI;

public class ToolChecker : MonoBehaviour
{
    public Toggle toolCursor, toolBuild, toolRemove;

    void Start()
    {
        
    }

    void Update()
    {
        if (toolCursor.isOn)
        {
            if (Tool.getSelected() != Tool.selection.CURSOR)
            {
                Tool.setSelection(Tool.selection.CURSOR);
            }
        }
        else if (toolBuild.isOn)
        {
            if (Tool.getSelected() != Tool.selection.BUILD)
            {
                Tool.setSelection(Tool.selection.BUILD);
            }
        }
        else if (toolRemove.isOn)
        {
            if (Tool.getSelected() != Tool.selection.REMOVE)
            {
                Tool.setSelection(Tool.selection.REMOVE);
            }
        }
    }
}
