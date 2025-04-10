<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use App\Models\User;
use Illuminate\Support\Facades\Hash;

class AuthController extends Controller
{
    public function showRegisterForm()
    {
        return view('reg');
    }

    public function register(Request $request)
    {
        $request->validate([
            'name' => 'required|string|max:255',
            'email' => 'required|string|email|unique:users',
            'password' => 'required|string|confirmed',
        ]);

        User::create([
            'name' => $request->name,
            'email' => $request->email,
            'password' => Hash::make($request->password),
        ]);

        return redirect()->route('login');
    }

    public function showLoginForm()
    {
        return view('auth');
    }

    // public function login(Request $request)
    // {
    //     $credentials = $request->validate([
    //         'name' => 'required|string',
    //         'password' => 'required|string',
    //     ]);

    //     if (Auth::attempt(['name' => $credentials['name'], 'password' => $credentials['password']])) {
    //         return redirect()->intended('/dashboard');
    //     }

    //     return back()->withErrors(['name' => 'Неверное имя или пароль']);
    // }

    public function login(Request $request)
    {
        $credentials = $request->validate([
            'name' => 'required|string',
            'password' => 'required|string',
        ]);

        if (Auth::attempt(['name' => $credentials['name'], 'password' => $credentials['password']])) {
            return redirect()->route('home');
        }

        return back()->withErrors(['name' => 'Неверное имя или пароль']);
    }

    public function logout(Request $request)
    {
        Auth::logout();
        return redirect()->route('login');
    }
}
