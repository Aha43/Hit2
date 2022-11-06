# HIT2 - Hierarchial Integration Test Framework

Work very much in progress, no usable code here yet, but plan is....

I made [HIT](https://github.com/Aha43/Hit), a framework for integration testing that I have used now in some projects... it works and gives me what I want but the approach I took defining hierarchy of tests on attributes I find bit hard to maintain, communicate to developers and easily relate to when returning to a test project using HIT after a while. Here I plan to experiment with an approach to define the hierarchy in code (c# code). See how that goes!

This is framework for itegration testing where
- *Actors* (implementations of the *IActor* interface) act on the system being testet
- Actors make claims about the system they have acted on: what now to be true about the system
- Actors are organized to act in a sequence called a user story
-   
