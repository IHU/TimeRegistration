import * as React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Projects from "./views/Projects";
import Project from "./views/Project";
import "./style.css";

export default function App() {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <a className="navbar-brand" href="/">
                        Timelogger
                    </a>
                </div>
            </header>

            <main>
                <div className="container mx-auto">
                    <Router>
                        <Routes>
                            <Route path="/project/:id/" element={<Project />} />
                            <Route path="*" element={<Projects />} />
                        </Routes>
                    </Router>
                </div>
            </main>
        </>
    );
}
