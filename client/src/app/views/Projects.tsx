import React from "react";
import ListProjects from "../components/ListProjects";
import TimeLogEntry from "../components/TimeLogEntry";
import Search from '../components/Search';

export default function Projects() {

    const [searchTerm, setSearchTerm] = React.useState('');

    const handleSearch = (searchTerm) => {
        setSearchTerm(searchTerm);
    };
    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <TimeLogEntry />
                </div>
                <div className="w-1/2 flex justify-end">
                    <Search onSearch={handleSearch} />
                </div>
            </div>
            <ListProjects searchQuery={searchTerm}/>
        </>
    );
}
