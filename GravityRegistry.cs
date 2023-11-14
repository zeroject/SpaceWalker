using System.Collections.Generic;
using System.Dynamic;
using Godot;

public static class GravityRegistry {
    public static List<GravitySubject> gravitySubjects {get; private set;} = new List<GravitySubject>();

    public static void AddGravitySubject(GravitySubject subject) {
        gravitySubjects.Add(subject);
    }

    public static void RemoveGravitySubject(GravitySubject subject) {
        gravitySubjects.Remove(subject);
    }
}