import React, { useState } from 'react';

const SearchComponent = ({ onSearch }) => {
    const [searchTerm, setSearchTerm] = useState('');

    const handleInputChange = (e) => {
        setSearchTerm(e.target.value);
    };

    const handleSearchClick = () => {
        // Call the parent component's search function with the search term
        onSearch(searchTerm);
    };

    return (
        <div>
            <input className="border rounded-full py-2 px-4" type="search" aria-label="Search"
                placeholder="Search by project Id"
                value={searchTerm}
                onChange={handleInputChange} />
            <input type="button" onClick={handleSearchClick} className="shadow bg-blue-500 font-bold py-2 px-4 border border-blue-500 rounded text-slate-100 hover:text-blue-800" value="Search" />

        </div>

    );
};

export default SearchComponent;