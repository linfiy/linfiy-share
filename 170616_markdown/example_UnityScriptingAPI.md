# Array

[Other Versions]()


## Description

Array allow you to store mutiple objects in single variable.

The Array class is only available in Javascript.

Here is basic example of what you can do with an array class:

> The [Array]() class is only available in JavaScript.

There are two types of arrays in Unity, Builtin arrays and normal JavaScript Arrays, Builin arrays(native .NET arrays), are extremely fast and efficient but they can not be resized.

They are statically typed which allows them to be edited in the inspector. Here is a basic example of how you can use builin arrays:

``` csharp
// example c# script showing how
// an array can be implemented.

using UnityEngine;
using System.Collections;

public class ExampleClass: MonoBehaviour {
  // Exposes an float array in the inspector, which you can edit there.
  public float[] values;

  void Start () {
    foreach (float value in values) {
      print(value);
    }

    // Since we can't resize builtin arrays 
    // we have to recreate the array to resize it
    values = new float[10];

    // assign the second element 
    value[1] = 5.0F;
  }
}
```

Builtin arrays are useful in performance critical code(WITH Unity's javascript and builtin arrays you could easily process 2 million vertices using the [mesh interface]() in one second.)

Normal Javascript Arrays on the other hand can be resizied, sorted and can do all other operations you  would expect from an array class. Javascript Arrays do not show up in the inspector. Note: You can easily convert between Javascript Arrays and builtin arrays.

> The [Array]() class is only available in JavaScript.

Note that Array functions are upper case following Unity's naming convention. As a convenience for javascript users, Unity also accepts lower case functions for the array class.

**Note:** Unity doesn't support serialization of a List of Lists, nor an Array of Arrays.

## Variables

|||
:--|:--
[length]()|The Length property of the array that returns or sets the number of elements in array.

## Constructors

|||
:--|:--
[Array]()|Create an Array of a fixed size.

## Public Functions

|||
:--|:--
[Add]()|Adds value to the end of the array.
[Clear]()|Empties the array. The length of the array will be zero.
[Concat]()|Concat joins two or more arrays.
[Join]()|Join the contents of an array into one string.
[Pop]()|Removes the last element of the array and returns it.
[Push]()|Adds value to the end of the array.
[RemoveAt]()|Removes the element at index from the array.
[Shift]()|Removes the first element of the array and returns it.
[Sort]()|Sorts all Array elements.
[Unshift]()|Unshift adds one or more elements to the beginning of an array and returns the new length of the array.

---

**[Leave Feedback]()**

Is something described here not working as you expect it to? It might be a **Known Issue**. Please check with the Issue Tracker at **[issuetracker.unity3d.com]()**.

Copyright © 2017 Unity Technologies. Publication: 5.6-001L. Built: 2017-06-02.


[Tutorials](), [Community Answers](), [Knowledge Base](), [Forums]() [Asset Store]()