#pragma once

#ifdef _WIN32
  #define API __declspec(dllexport)
#else
  #define API
#endif

#ifdef __cplusplus
extern "C"
{
#endif

// API double second();
API float jacobi(int);
API void initmt();
API double fflop(int,int,int);
API double mflops(int,double,double);

#ifdef __cplusplus
}
#endif
