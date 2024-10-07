﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You want nodes to have customized information, but you don't want to write it directly into the EasyDialogueNode class.
/// This is because, if I update the class in the future, your stuff will be overwritten!
/// Here is a safe space to place any custom information that you care about, and you can put it right into your nodes. And I won't update it!
/// </summary>
[System.Serializable]
public class CustomNodeInfo
{
}
