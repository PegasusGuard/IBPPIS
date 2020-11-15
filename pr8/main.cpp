#include <iostream>
#include <vector>


using namespace std;

string toBinary(int n);

int main()
{
    string myString = "I am a shitlord";
    vector<char> bytes(myString.begin(), myString.end());
    bytes.push_back('\0');
    char *c = &bytes[0];
    cout << "The ASCII value of " << myString << " is " << c;
    return 0;
}

string toBinary(int n)
{
    string r;
    while(n!=0) {r=(n%2==0 ?"0":"1")+r; n/=2;}
    return r;
}
