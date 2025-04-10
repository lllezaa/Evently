<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AuthController;


Route::get('/', function () {
    return view('home');
})->name('home');

// Route::get('/', function () {
//     return view('home');
// })->middleware('auth')->name('home');

Route::get('/register', [AuthController::class, 'showRegisterForm']);
Route::post('/register', [AuthController::class, 'register'])->name('register');

Route::get('/login', [AuthController::class, 'showLoginForm']);
Route::post('/login', [AuthController::class, 'login'])->name('login');


Route::post('/logout', [AuthController::class, 'logout'])->name('logout')->middleware('auth');