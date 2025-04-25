#ifndef MATH_LIBRARY_H
#define MATH_LIBRARY_H

#ifdef __cplusplus
extern "C" {
#endif

#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define MATHLIBRARY_API __declspec(dllimport)
#endif

struct Vector3 {
    double x;
    double y;
    double z;
};

MATHLIBRARY_API double Add(double a, double b);
MATHLIBRARY_API double Subtract(double a, double b);
MATHLIBRARY_API double Multiply(double a, double b);
MATHLIBRARY_API double Divide(double a, double b);
MATHLIBRARY_API double Power(double base, double exponent);
MATHLIBRARY_API double SquareRoot(double value);
MATHLIBRARY_API Vector3 CrossProduct(Vector3 a, Vector3 b);
MATHLIBRARY_API double DotProduct(Vector3 a, Vector3 b);
MATHLIBRARY_API Vector3 Normalize(Vector3 v);
MATHLIBRARY_API double GetLength(Vector3 v);

#ifdef __cplusplus
}
#endif

#endif