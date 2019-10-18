# Face Morphing Program

&#8195;&#8195;This project is the second task for THU DA 2019 Fall Numerical Analysis Project 1, an face morphing program that transform between human faces using TPS transform based on face-landmarks.  

## Intro
&#8195;&#8195;The coordinate transform algorithm for this project is TPS (Thin Plate Spline), which compute a transfer vector based on two sets of points.
The function of this algorithm is to do non-rigid transform (for example rotation, distortion) for an image, which can't be demonstrated by a transform matrix or regular formula.
The method is to minimize an energy function which has analytical solution. For detailed information about the TPS algorithm, I recommend you to go to [Wiki-Thin plate spline](https://en.wikipedia.org/wiki/Thin_plate_spline).  

## Prerequisites
&#8195;&#8195;The project is developed in C#, .NET Framework 4.6.1 using Visual Studio 2017. Other packages include:
- OpenCvSharp3-AnyCPU.3.4.1.20180319
- DlibDotNet.19.18.0.20190928  
You can simply install these two packages via NuGet package maneger in Visual Studio.  

## Quick Start
&#8195;&#8195;To get a quick start of this repo, I recommend you to open the FaceMorphing.sln in VS then run it! You will see the UI like:  
  
<img src="" width=600 alt="UI">
  
  
&#8195;&#8195;So let me give a brief description of this UI:
- You can set the interpolation method including nearest, bilinear and bicubic
- You can select an input image and a reference image via path-to-image. The landmarks of input image will be used to compute a transfer vector towards reference image landmarks. Then the input image will be transformed using this vector
- You can display face-landmarks directly on face when cilcking Show Keypoints
- After you have finished all above, click Transform and see the result!  

## About the code
&#8195;&#8195;A simple description about the code files:
- Form1.cs is the UI file (and it's quite ugly OTZ)
- Transform.cs is the class for transform and interpolation algorithms
- Detector.cs is the class that provide interface for detect face-landmarks from image
- Matrix2d.cs is my own implementation of a 2d matrix. That's because we are not allowed to use matrix class from packages like OpenCV or Eigen. So in this class I implement some simple operations like inverse, swap rows and columns, etc  

## Results
&#8195;&#8195;Show one result for clarity.  
Trump1 to Trump2 using nearest:  
  
<img src="" width=400 alt="ex1">  
  
