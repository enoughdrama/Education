#include <windows.h>
#include <iostream>
#include <iomanip>

struct Vector3 {
    double x;
    double y;
    double z;
};

typedef double (*AddFunc)(double, double);
typedef double (*SubtractFunc)(double, double);
typedef double (*MultiplyFunc)(double, double);
typedef double (*DivideFunc)(double, double);
typedef double (*PowerFunc)(double, double);
typedef double (*SquareRootFunc)(double);
typedef Vector3 (*CrossProductFunc)(Vector3, Vector3);
typedef double (*DotProductFunc)(Vector3, Vector3);
typedef Vector3 (*NormalizeFunc)(Vector3);
typedef double (*GetLengthFunc)(Vector3);

void PrintVector(const Vector3& v) {
    std::cout << std::fixed << std::setprecision(4);
    std::cout << "(" << v.x << ", " << v.y << ", " << v.z << ")";
}

int main() {
    HMODULE hMathLibrary = LoadLibrary("MathLibrary.dll");
    
    if (!hMathLibrary) {
        std::cerr << "Failed to load MathLibrary.dll. Error code: " << GetLastError() << std::endl;
        return 1;
    }
    
    AddFunc add = (AddFunc)GetProcAddress(hMathLibrary, "Add");
    SubtractFunc subtract = (SubtractFunc)GetProcAddress(hMathLibrary, "Subtract");
    MultiplyFunc multiply = (MultiplyFunc)GetProcAddress(hMathLibrary, "Multiply");
    DivideFunc divide = (DivideFunc)GetProcAddress(hMathLibrary, "Divide");
    PowerFunc power = (PowerFunc)GetProcAddress(hMathLibrary, "Power");
    SquareRootFunc sqrt = (SquareRootFunc)GetProcAddress(hMathLibrary, "SquareRoot");
    CrossProductFunc cross = (CrossProductFunc)GetProcAddress(hMathLibrary, "CrossProduct");
    DotProductFunc dot = (DotProductFunc)GetProcAddress(hMathLibrary, "DotProduct");
    NormalizeFunc normalize = (NormalizeFunc)GetProcAddress(hMathLibrary, "Normalize");
    GetLengthFunc getLength = (GetLengthFunc)GetProcAddress(hMathLibrary, "GetLength");
    
    if (!add || !subtract || !multiply || !divide || !power || 
        !sqrt || !cross || !dot || !normalize || !getLength) {
        std::cerr << "Failed to get function addresses. Error code: " << GetLastError() << std::endl;
        FreeLibrary(hMathLibrary);
        return 1;
    }
    
    std::cout << std::fixed << std::setprecision(4);
    
    std::cout << "Basic Math Operations:" << std::endl;
    std::cout << "Add(5.7, 3.2) = " << add(5.7, 3.2) << std::endl;
    std::cout << "Subtract(10.5, 4.2) = " << subtract(10.5, 4.2) << std::endl;
    std::cout << "Multiply(3.5, 2.5) = " << multiply(3.5, 2.5) << std::endl;
    std::cout << "Divide(10.0, 2.5) = " << divide(10.0, 2.5) << std::endl;
    std::cout << "Power(2.0, 3.0) = " << power(2.0, 3.0) << std::endl;
    std::cout << "SquareRoot(16.0) = " << sqrt(16.0) << std::endl;
    std::cout << std::endl;
    
    Vector3 v1 = {1.0, 2.0, 3.0};
    Vector3 v2 = {4.0, 5.0, 6.0};
    
    std::cout << "Vector Operations:" << std::endl;
    std::cout << "Vector v1 = ";
    PrintVector(v1);
    std::cout << std::endl;
    
    std::cout << "Vector v2 = ";
    PrintVector(v2);
    std::cout << std::endl;
    
    std::cout << "DotProduct(v1, v2) = " << dot(v1, v2) << std::endl;
    
    Vector3 cross_result = cross(v1, v2);
    std::cout << "CrossProduct(v1, v2) = ";
    PrintVector(cross_result);
    std::cout << std::endl;
    
    std::cout << "Length of v1 = " << getLength(v1) << std::endl;
    
    Vector3 norm_v1 = normalize(v1);
    std::cout << "Normalized v1 = ";
    PrintVector(norm_v1);
    std::cout << std::endl;
    std::cout << "Length of normalized v1 = " << getLength(norm_v1) << std::endl;
    
    FreeLibrary(hMathLibrary);
    
    std::cout << "\nPress Enter to exit...";
    std::cin.get();
    return 0;
}