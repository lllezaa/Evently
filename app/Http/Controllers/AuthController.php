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
            'lastname' => 'required|string|max:255',
            'email' => 'required|string|email|unique:users',
            'login' => 'required|string|max:255|unique:users',
            'password' => 'required|string|confirmed',
        ]);

        User::create([
            'name' => $request->name,
            'lastname' => $request->lastname,
            'email' => $request->email,
            'login' => $request->login,
            'password' => Hash::make($request->password),
            'roles_id' => 3,
        ]);

        return redirect()->route('login');
    }

    public function showLoginForm()
    {
        return view('auth');
    }


    public function login(Request $request)
    {
        $credentials = $request->validate([
            'login' => 'required|string',
            'password' => 'required|string',
        ]);

        if (Auth::attempt(['login' => $credentials['login'], 'password' => $credentials['password']])) {
            return redirect()->route('home');
        }

        return back()->withErrors(['login' => 'Неверное имя или пароль']);
    }

    public function logout(Request $request)
    {
        Auth::logout();
        return redirect()->route('login');
    }
}
