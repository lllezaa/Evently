<?php

use App\Http\Controllers\AdminController;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\EventController;


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


Route::get('/', [EventController::class, 'index'])->name('home'); //не удалять нужно для получения событий
Route::get('/create-event', [EventController::class, 'create'])->name('create.event');
Route::post('/store-event', [EventController::class, 'store'])->name('store.event');

Route::middleware('auth')->group(function () {
    Route::get('/admin/events', [AdminController::class, 'editEvents'])->name('admin.events');
    Route::post('/admin/events/update/{id}', [AdminController::class, 'updateEvent'])->name('admin.events.update');
    Route::post('/admin/events/delete/{id}', [AdminController::class, 'deleteEvent'])->name('admin.events.delete');

    Route::get('/admin/users', [AdminController::class, 'editUsers'])->name('admin.users');
    Route::post('/admin/users/update/{id}', [AdminController::class, 'updateUser'])->name('admin.users.update');
    Route::post('/admin/users/delete/{id}', [AdminController::class, 'deleteUser'])->name('admin.users.delete');
});

Route::get('/event/{id}', [EventController::class, 'show'])->name('event.show');
Route::post('/event/{id}/record', [EventController::class, 'record'])->name('event.record');
Route::delete('/event/{id}/unrecord', [EventController::class, 'unrecord'])->name('event.unrecord');
